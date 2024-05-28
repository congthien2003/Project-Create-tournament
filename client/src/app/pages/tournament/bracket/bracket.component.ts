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
		const quantityTeam = tour.quantityTeam;
		// temp is quantity team
		let temp = quantityTeam;
		let quantityMatch = 0;
		while (temp % 2 !== 1) {
			quantityMatch += temp / 2;
			temp = temp / 2;
		}

		switch (quantityMatch) {
			case 1: {
				listMatch[0]
					? this.final.matchs.push(listMatch[0])
					: this.final.matchs.push({ ...new Match() });
				break;
			}
			case 3: {
				for (let i = 0; i < 2; i++) {
					this.semifinals.matchs.push(listMatch[i]);
				}
				listMatch[2]
					? this.final.matchs.push(listMatch[2])
					: this.final.matchs.push({ ...new Match() });
				break;
			}
			case 7: {
				for (let i = 0; i < 4; i++) {
					this.quarterfinals.matchs[i] = listMatch[i];
				}
				listMatch[4]
					? (this.semifinals.matchs[0] = listMatch[4])
					: (this.semifinals.matchs[0] = { ...new Match() });
				listMatch[5]
					? (this.semifinals.matchs[1] = listMatch[5])
					: (this.semifinals.matchs[1] = { ...new Match() });
				listMatch[6]
					? (this.final.matchs[0] = listMatch[6])
					: (this.final.matchs[0] = { ...new Match() });
				break;
			}
		}
	}

	/* 
		TODO: Function load match for this.listMatchs
		TODO: Function update match with this.listMatchs
		TODO: Validations Forms 

		!: Hehe
		?: Hehe
	*/

	// ! Update Component Data
	updateDataConverted(tour: Tournament, listMatch: Match[]): void {}

	handleUpdateMatch(data: MatchResult): void {
		const matchResult = Object.create({ data: data });

		if (
			this.quarterfinals.matchs.find(
				(x) => x.id === matchResult.data.matchId
			)
		) {
			// type quarterfinals
			this.updateSemiFinals(data);
		} else if (
			this.semifinals.matchs.find(
				(x) => x.id === matchResult.data.matchId
			)
		) {
			// type Semifinals
			this.updateFinal(data);
		} else if (
			this.final.matchs.find((x) => x.id === matchResult.data.matchId)
		) {
			this.loadMatch();
		}
	}

	updateQuaterFinals(data: MatchResult): void {
		const matchResult = Object.create({ data: data });

		if (matchResult.idMatch == this.round16.matchs[0].id) {
			this.quarterfinals.matchs[0].idTeam1 = matchResult.idTeamWin;
			this.quarterfinals.matchs[0].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[0].id == 0) {
				this.quarterfinals.matchs[0] = this.createMatch(
					this.quarterfinals.matchs[0]
				);
			} else {
				this.quarterfinals.matchs[0] = this.updateMatch(
					this.quarterfinals.matchs[0]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[1].id) {
			this.quarterfinals.matchs[0].idTeam2 = matchResult.idTeamWin;
			this.quarterfinals.matchs[0].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[0].id == 0) {
				this.quarterfinals.matchs[0] = this.createMatch(
					this.quarterfinals.matchs[0]
				);
			} else {
				this.quarterfinals.matchs[0] = this.updateMatch(
					this.quarterfinals.matchs[0]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[2].id) {
			this.quarterfinals.matchs[1].idTeam1 = matchResult.idTeamWin;
			this.quarterfinals.matchs[1].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[1].id == 0) {
				this.quarterfinals.matchs[1] = this.createMatch(
					this.quarterfinals.matchs[1]
				);
			} else {
				this.quarterfinals.matchs[1] = this.updateMatch(
					this.quarterfinals.matchs[1]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[3].id) {
			this.quarterfinals.matchs[1].idTeam2 = matchResult.idTeamWin;
			this.quarterfinals.matchs[1].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[1].id == 0) {
				this.quarterfinals.matchs[1] = this.createMatch(
					this.quarterfinals.matchs[1]
				);
			} else {
				this.quarterfinals.matchs[1] = this.updateMatch(
					this.quarterfinals.matchs[1]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[4].id) {
			this.quarterfinals.matchs[2].idTeam2 = matchResult.idTeamWin;
			this.quarterfinals.matchs[2].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[2].id == 0) {
				this.quarterfinals.matchs[2] = this.createMatch(
					this.quarterfinals.matchs[2]
				);
			} else {
				this.quarterfinals.matchs[2] = this.updateMatch(
					this.quarterfinals.matchs[2]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[5].id) {
			this.quarterfinals.matchs[2].idTeam2 = matchResult.idTeamWin;
			this.quarterfinals.matchs[2].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[2].id == 0) {
				this.quarterfinals.matchs[2] = this.createMatch(
					this.quarterfinals.matchs[2]
				);
			} else {
				this.quarterfinals.matchs[2] = this.updateMatch(
					this.quarterfinals.matchs[2]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[6].id) {
			this.quarterfinals.matchs[3].idTeam2 = matchResult.idTeamWin;
			this.quarterfinals.matchs[3].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[3].id == 0) {
				this.quarterfinals.matchs[3] = this.createMatch(
					this.quarterfinals.matchs[3]
				);
			} else {
				this.quarterfinals.matchs[3] = this.updateMatch(
					this.quarterfinals.matchs[3]
				);
			}
		} else if (matchResult.idMatch == this.round16.matchs[7].id) {
			this.quarterfinals.matchs[3].idTeam2 = matchResult.idTeamWin;
			this.quarterfinals.matchs[3].tournamentId = this.idTour;

			if (this.quarterfinals.matchs[3].id == 0) {
				this.quarterfinals.matchs[3] = this.createMatch(
					this.quarterfinals.matchs[3]
				);
			} else {
				this.quarterfinals.matchs[3] = this.updateMatch(
					this.quarterfinals.matchs[3]
				);
			}
		}
	}

	updateSemiFinals(data: MatchResult): void {
		if (data.matchId === this.quarterfinals.matchs[0].id) {
			this.semifinals.matchs[0].idTeam1 = data.idTeamWin;
			this.semifinals.matchs[0].tournamentId = this.idTour;

			if (
				this.semifinals.matchs[0].idTeam1 != 0 &&
				this.semifinals.matchs[0].idTeam2 != 0
			) {
				if (this.semifinals.matchs[0].id === 0) {
					this.semifinals.matchs[0] = this.createMatch(
						this.semifinals.matchs[0]
					);
				} else {
					this.semifinals.matchs[0] = this.updateMatch(
						this.semifinals.matchs[0]
					);
				}
			}
			return;
		}
		if (data.matchId === this.quarterfinals.matchs[1].id) {
			this.semifinals.matchs[0].idTeam2 = data.idTeamWin;
			this.semifinals.matchs[0].tournamentId = this.idTour;

			if (
				this.semifinals.matchs[0].idTeam1 != 0 &&
				this.semifinals.matchs[0].idTeam2 != 0
			) {
				if (this.semifinals.matchs[0].id == 0) {
					this.semifinals.matchs[0] = this.createMatch(
						this.semifinals.matchs[0]
					);
				} else {
					this.semifinals.matchs[0] = this.updateMatch(
						this.semifinals.matchs[0]
					);
				}
			}
			return;
		}
		if (data.matchId === this.quarterfinals.matchs[2].id) {
			this.semifinals.matchs[1].idTeam1 = data.idTeamWin;
			this.semifinals.matchs[1].tournamentId = this.idTour;

			if (
				this.semifinals.matchs[1].idTeam1 != 0 &&
				this.semifinals.matchs[1].idTeam2 != 0
			) {
				if (this.semifinals.matchs[1].id == 0) {
					this.semifinals.matchs[1] = this.createMatch(
						this.semifinals.matchs[1]
					);
				} else {
					this.semifinals.matchs[1] = this.updateMatch(
						this.semifinals.matchs[1]
					);
				}
			}
			return;
		}
		if (data.matchId === this.quarterfinals.matchs[3].id) {
			this.semifinals.matchs[1].idTeam2 = data.idTeamWin;
			this.semifinals.matchs[1].tournamentId = this.idTour;

			if (
				this.semifinals.matchs[1].idTeam1 != 0 &&
				this.semifinals.matchs[1].idTeam2 != 0
			) {
				if (this.semifinals.matchs[1].id == 0) {
					this.semifinals.matchs[1] = this.createMatch(
						this.semifinals.matchs[1]
					);
				} else {
					this.semifinals.matchs[1] = this.updateMatch(
						this.semifinals.matchs[1]
					);
				}
			}

			return;
		}
	}

	updateFinal(data: MatchResult): void {
		this.final.matchs[0].tournamentId = this.idTour;

		if (data.matchId == this.semifinals.matchs[0].id) {
			this.final.matchs[0].idTeam1 = data.idTeamWin;
		} else {
			this.final.matchs[0].idTeam2 = data.idTeamWin;
		}
		if (
			this.final.matchs[0].idTeam1 != 0 &&
			this.final.matchs[0].idTeam2 != 0
		) {
			if (this.final.matchs[0].id == 0) {
				this.final.matchs[0] = this.createMatch(this.final.matchs[0]);
			} else {
				this.final.matchs[0] = this.updateMatch(this.final.matchs[0]);
			}
		}
	}

	updateMatch(match: Match): Match {
		this.matchService.update(match.id, match).subscribe({
			next: (value) => {
				this.toastr.success("", "Cập nhật thành công !", {
					timeOut: 3000,
				});
				this.loadMatch();

				return value;
			},
		});
		return this.tempMatch;
	}

	createMatch(match: Match): Match {
		this.matchService.create(match).subscribe({
			next: (value) => {
				this.loadMatch();

				return value;
			},
			error: (error) => {},
		});
		return this.tempMatch;
	}
}

export class BracketType {
	title: string;
	matchs: Match[];
}
