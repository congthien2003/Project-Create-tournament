import { Component, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { Subscription } from "rxjs";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { MatchService } from "src/app/core/services/match.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { CaneditService } from "src/app/shared/services/canedit.service";
import { IdloaderService } from "src/app/shared/services/idloader.service";
import { ToastrService } from "ngx-toastr";
import { MatchResultService } from "src/app/core/services/match-result.service";
import { TypeOfMatch } from "src/app/core/constant/data/typeofMatch.data";
import JSConfetti from "js-confetti";
import { MatDialog } from "@angular/material/dialog";
import { CongratulationsComponent } from "src/app/shared/components/congratulations/congratulations.component";
import { Team } from "src/app/core/models/classes/Team";

@Component({
	selector: "app-bracket",
	templateUrl: "./bracket.component.html",
	styleUrls: ["./bracket.component.scss"],
})
export class BracketComponent implements OnInit {
	idTour: number;
	isAuthenticated: boolean = false;
	canEdit: boolean = false;
	private subscription: Subscription;

	tour: Tournament;
	tempMatch: Match = new Match();
	listMatch: Match[] = [];
	round32: BracketType = {
		title: "Vòng 32",
		matchs: [],
	};

	round16: BracketType = {
		title: "Vòng bảng",
		matchs: [],
	};

	quarterfinals: BracketType = {
		title: "Tứ kết",
		matchs: [],
	};

	semifinals: BracketType = {
		title: "Bán kết",
		matchs: [],
	};

	final: BracketType = {
		title: "Chung kết",
		matchs: [],
	};

	constructor(
		private tourService: TournamentService,
		private matchService: MatchService,
		private dialogRef: MatDialog,
		private idloaderService: IdloaderService,
		private canEditService: CaneditService,
		private toastr: ToastrService
	) {}

	ngOnInit(): void {
		this.subscription = this.idloaderService.currentId$.subscribe({
			next: (value) => {
				this.idTour = value ?? 0;
			},
		});

		this.tourService.getById(this.idTour).subscribe({
			next: (value) => {
				this.tour = value;

				this.canEdit = this.canEditService.canEdit(this.tour);

				this.loadMatch();
			},
		});
	}

	loadMatch(): void {
		this.matchService.list(this.idTour).subscribe({
			next: (value) => {
				this.listMatch = value;

				this.convertData(this.tour, this.listMatch);
			},
		});
	}

	convertData(tour: Tournament, listMatch: Match[]): void {
		const typeOfMatch = TypeOfMatch;
		this.round32.matchs = listMatch.filter(
			(x) => x.round == typeOfMatch.round32
		);

		this.round16.matchs = listMatch.filter(
			(x) => x.round == typeOfMatch.round16
		);
		this.quarterfinals.matchs = listMatch.filter(
			(x) => x.round == typeOfMatch.quaterfinal
		);
		this.semifinals.matchs = listMatch.filter(
			(x) => x.round == typeOfMatch.semifinal
		);
		this.final.matchs = listMatch.filter(
			(x) => x.round == typeOfMatch.final
		);
	}

	/* 
		TODO: Function load match for this.listMatchs
		TODO: Function update match with this.listMatchs
		TODO: Validations Forms 

		!: Hehe
		?: Hehe
	*/

	handleUpdateMatch($event: any): void {
		if (!$event.update) {
			this.loadMatch();
		}
		if ($event.round === 5) {
			const jsConfetti = new JSConfetti();
			this.openDialogCongra($event.team);

			jsConfetti.addConfetti();
			setTimeout(() => {
				jsConfetti.clearCanvas();
				this.dialogRef.closeAll();
			}, 3000);
		}
	}

	openDialogCongra(team: Team): void {
		const dialogRef = this.dialogRef.open(CongratulationsComponent, {
			data: {
				team,
			},
		});
	}
}

export class BracketType {
	title: string;
	matchs: Match[];
}
