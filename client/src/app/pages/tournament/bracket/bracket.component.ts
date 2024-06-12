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

				console.log(this.quarterfinals.matchs.length);
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
			case 15: {
				for (let i = 0; i < 8; i++) {
					this.round16.matchs[i] = listMatch[i];
				}

				let z = 0;
				for (let j = 8; j < 12; j++) {
					console.log(listMatch[j]);

					listMatch[j] !== undefined
						? (this.quarterfinals.matchs[z++] = listMatch[j])
						: (this.quarterfinals.matchs[z++] = {
								...new Match(),
						  });
				}

				listMatch[12] !== undefined
					? (this.semifinals.matchs[0] = listMatch[12])
					: (this.semifinals.matchs[0] = { ...new Match() });
				listMatch[13] !== undefined
					? (this.semifinals.matchs[1] = listMatch[13])
					: (this.semifinals.matchs[1] = { ...new Match() });

				listMatch[14] !== undefined
					? (this.final.matchs[0] = listMatch[14])
					: (this.final.matchs[0] = { ...new Match() });

				break;
			}
			case 31: {
				let z = 0;

				for (let k = 0; k < 16; k++) {
					this.round32.matchs[k] = listMatch[z++];
				}

				for (let i = 0; i < 8; i++) {
					this.round16.matchs[i] = listMatch[z++];
				}

				for (let j = 0; j < 4; j++) {
					listMatch[j]
						? (this.quarterfinals.matchs[z++] = listMatch[j])
						: (this.quarterfinals.matchs[z++] = {
								...new Match(),
						  });
				}

				listMatch[z++] !== undefined
					? (this.semifinals.matchs[0] = listMatch[z++])
					: (this.semifinals.matchs[0] = { ...new Match() });
				listMatch[z++] !== undefined
					? (this.semifinals.matchs[1] = listMatch[z++])
					: (this.semifinals.matchs[1] = { ...new Match() });

				listMatch[z++] !== undefined
					? (this.final.matchs[0] = listMatch[z++])
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

export const listMatch = [
	{
		id: 1137,
		idTeam1: 1204,
		idTeam2: 1205,
		round: 0,
		created: "2024-06-02T15:53:04.5356761",
		startAt: "2024-06-02T15:53:04.5356739",
		tournamentId: 1050,
	},
	{
		id: 1138,
		idTeam1: 1206,
		idTeam2: 1207,
		round: 0,
		created: "2024-06-02T15:53:04.5568415",
		startAt: "2024-06-02T15:53:04.5568404",
		tournamentId: 1050,
	},
	{
		id: 1139,
		idTeam1: 1208,
		idTeam2: 1209,
		round: 0,
		created: "2024-06-02T15:53:04.5593896",
		startAt: "2024-06-02T15:53:04.559389",
		tournamentId: 1050,
	},
	{
		id: 1140,
		idTeam1: 1210,
		idTeam2: 1211,
		round: 0,
		created: "2024-06-02T15:53:04.5612961",
		startAt: "2024-06-02T15:53:04.5612954",
		tournamentId: 1050,
	},
	{
		id: 1141,
		idTeam1: 1212,
		idTeam2: 1213,
		round: 0,
		created: "2024-06-02T15:53:04.5632691",
		startAt: "2024-06-02T15:53:04.5632684",
		tournamentId: 1050,
	},
	{
		id: 1142,
		idTeam1: 1214,
		idTeam2: 1215,
		round: 0,
		created: "2024-06-02T15:53:04.5651376",
		startAt: "2024-06-02T15:53:04.5651368",
		tournamentId: 1050,
	},
	{
		id: 1143,
		idTeam1: 1216,
		idTeam2: 1217,
		round: 0,
		created: "2024-06-02T15:53:04.5670346",
		startAt: "2024-06-02T15:53:04.567034",
		tournamentId: 1050,
	},
	{
		id: 1144,
		idTeam1: 1218,
		idTeam2: 1219,
		round: 0,
		created: "2024-06-02T15:53:04.5688386",
		startAt: "2024-06-02T15:53:04.5688375",
		tournamentId: 1050,
	},
];
