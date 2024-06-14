import {
	Component,
	EventEmitter,
	Inject,
	Input,
	OnInit,
	Output,
} from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { Subscription } from "rxjs";
import { Match } from "src/app/core/models/classes/Match";
import { Team } from "src/app/core/models/classes/Team";
import { MatchService } from "src/app/core/services/match.service";
import { TeamService } from "src/app/core/services/team.service";
import { IdloaderService } from "src/app/shared/services/idloader.service";

@Component({
	selector: "app-form-edit-info",
	templateUrl: "./form-edit-info.component.html",
	styleUrls: ["./form-edit-info.component.scss", "../form-edit.scss"],
})
export class FormEditInfoComponent implements OnInit {
	private subscription: Subscription;
	idTour: number;
	title = "Chọn đội";

	listTeam: Team[] = [];

	listTeam1: Team[] = [];
	listTeam2: Team[] = [];

	listTeamName1: string[] = [];
	listTeamName2: string[] = [];
	team1Choosed: any;
	team2Choosed: any;
	teamChoosedTemp: any = 0;

	constructor(
		private teamService: TeamService,
		private idloaderService: IdloaderService,
		private matchService: MatchService,
		@Inject(MAT_DIALOG_DATA)
		public data: {
			match: Match;
			team1: Team;
			team2: Team;
		}
	) {}
	ngOnInit(): void {
		this.team1Choosed = this.data.match.idTeam1;
		this.team2Choosed = this.data.match.idTeam2;
		this.teamChoosedTemp = 0;
		this.subscription = this.idloaderService.currentId$.subscribe({
			next: (value) => {
				this.idTour = value ?? 0;
				this.teamService.getAll(this.idTour).subscribe({
					next: (value) => {
						this.listTeam = value;
						this.loadDropdown();
					},
				});
			},
		});
		console.log(this.team1Choosed);
		console.log(this.team2Choosed);
	}

	loadDropdown(): void {
		console.log("Loaded dropdown");

		this.listTeam1 = this.listTeam.filter(
			(x) =>
				x.id !== this.team1Choosed &&
				x.id !== this.team2Choosed &&
				x.id !== this.teamChoosedTemp
		);
		this.listTeam2 = this.listTeam.filter(
			(x) =>
				x.id !== this.team1Choosed &&
				x.id !== this.team2Choosed &&
				x.id !== this.teamChoosedTemp
		);

		this.listTeamName1 = this.listTeam1.map((team) => team.name);
		this.listTeamName2 = this.listTeam2.map((team) => team.name);

		console.log(this.team1Choosed);
		console.log(this.team2Choosed);
	}

	@Output() saveInfo = new EventEmitter<any>();

	editMatch = new FormGroup({
		date: new FormControl<Date>(new Date(), Validators.required),
	});

	saveMatch(): void {
		const date = this.editMatch.get("date")?.value;

		this.saveInfo.emit({
			date,
		});

		this.data.match.idTeam1 = this.team1Choosed;
		this.data.match.idTeam2 = this.team2Choosed;

		// this.matchService
		// 	.update(this.data.match.id, this.data.match)
		// 	.subscribe({
		// 		next: (value) => {},
		// 	});
	}

	changeTeam($event: any, num: number) {
		console.log("Changed Team");

		if (num === 1) {
			this.teamChoosedTemp = this.team1Choosed;
			this.team1Choosed = this.listTeam1[$event].id;
		} else {
			this.teamChoosedTemp = this.team2Choosed;

			this.team2Choosed = this.listTeam2[$event].id;
		}

		console.log(this.team1Choosed);
		console.log(this.team2Choosed);

		this.loadDropdown();
	}
}
