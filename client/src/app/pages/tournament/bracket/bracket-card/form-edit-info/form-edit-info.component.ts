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
import { Subscription, filter, map } from "rxjs";
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

	team1Choosed: Team;
	team2Choosed: Team;
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
		this.team1Choosed = this.data.team1;
		this.team2Choosed = this.data.team2;
		this.teamChoosedTemp = 0;
		this.subscription = this.idloaderService.currentId$.subscribe({
			next: (value) => {
				this.idTour = value ?? 0;
				this.teamService
					.getListTeamSwap(this.idTour, this.data.match.round)
					.subscribe({
						next: (value) => {
							this.listTeam = value;
							this.loadDropdown();
						},
					});
			},
		});
	}

	loadDropdown(): void {
		this.listTeam1 = this.listTeam.filter(
			(x) =>
				x.id !== this.team1Choosed.id &&
				x.id !== this.team2Choosed.id &&
				x.id !== this.teamChoosedTemp
		);
		this.listTeam2 = this.listTeam.filter(
			(x) =>
				x.id !== this.team1Choosed.id &&
				x.id !== this.team2Choosed.id &&
				x.id !== this.teamChoosedTemp
		);
	}

	@Output() saveInfo = new EventEmitter<any>();

	editMatch = new FormGroup({
		date: new FormControl<Date>(new Date(), Validators.required),
	});

	saveMatch(): void {
		const date = this.editMatch.get("date")?.value;

		this.data.match.idTeam1 = this.team1Choosed.id;
		this.data.match.idTeam2 = this.team2Choosed.id;

		this.saveInfo.emit({
			date,
			match: this.data.match,
		});
	}

	changeTeam($event: any, num: number) {
		console.log("Changed Team");

		if (num === 1) {
			this.teamChoosedTemp = this.team1Choosed;
			this.team1Choosed.id = this.listTeam1[$event].id;
		} else {
			this.teamChoosedTemp = this.team2Choosed;

			this.team2Choosed.id = this.listTeam2[$event].id;
		}
		this.loadDropdown();
	}
}
