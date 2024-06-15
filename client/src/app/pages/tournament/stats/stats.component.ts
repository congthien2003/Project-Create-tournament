import { LiveAnnouncer } from "@angular/cdk/a11y";
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSort, Sort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { Subscription, forkJoin, map } from "rxjs";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { PlayerStats } from "src/app/core/models/classes/PlayerStats";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { MatchResultService } from "src/app/core/services/match-result.service";
import { MatchService } from "src/app/core/services/match.service";
import { PlayerService } from "src/app/core/services/player.service";
import { PlayerStatsService } from "src/app/core/services/playerstats.service";
import { TeamService } from "src/app/core/services/team.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { IdloaderService } from "src/app/shared/services/idloader.service";

@Component({
	selector: "app-stats",
	templateUrl: "./stats.component.html",
	styleUrls: ["./stats.component.scss"],
})
export class StatsComponent implements OnInit {
	idTour: number = 0;
	Tour: Tournament = new Tournament();
	listMatchResult: MatchResult[] = [];
	listStats: PlayerStats[] = [];
	isAuthenticated: boolean = false;
	canEdit: boolean = false;

	// Table
	displayedColumns: string[] = [
		"id",
		"playerId",
		"score",
		"redCard",
		"yellowCard",
	];
	tourStats!: any;
	teamStats: any[] = [];
	playerStats: any[] = [];

	teamStatsSource!: any;

	topScore!: any;

	private subscription: Subscription;

	constructor(
		private tourService: TournamentService,
		private authService: AuthenticationService,
		private matchService: MatchService,
		private matchResultService: MatchResultService,
		private playerStatsService: PlayerStatsService,
		private teamService: TeamService,
		private playerService: PlayerService,
		public dialog: MatDialog,
		private toastr: ToastrService,
		private idloaderService: IdloaderService
	) {}

	ngOnInit(): void {
		this.subscription = this.idloaderService.currentId$.subscribe({
			next: (id) => (this.idTour = id ?? 0),
		});

		this.tourService.getById(this.idTour).subscribe({
			next: (tour) => {
				this.Tour = tour;
				this.isAuthenticated = this.authService.isAuthenticated();
				const userId = this.authService.getUserIdFromToken();

				if (
					this.isAuthenticated === true &&
					userId === this.Tour.userId.toString()
				) {
					this.canEdit = true;
				}

				this.matchService.list(tour.id).subscribe({
					next: (listmatch) => {
						listmatch.forEach((match, index) => {
							this.matchResultService
								.getByIdMatch(match.id)
								.subscribe({
									next: (matchResult) => {
										this.listMatchResult[index] =
											matchResult;
									},
								});
						});
					},
				});

				// TODO: Table Player Stats, Team Stats

				// * Tour Stats
				this.playerStatsService
					.getTourStatsByIdTour(this.Tour.id)
					.subscribe({
						next: (res) => {
							const value = Object.values(res);
							this.tourStats = value[0];
						},
					});

				this.loadTeamStats();
			},
			error: (error) => {},
		});
	}

	// TABLE TEAM STATS

	tableTeamStats = {
		totalPage: 5,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 10,
		hasNext: true,
		hasPrev: false,
		sortColumn: "score",
		asc: true,
	};

	tablePlayerStats = {
		totalPage: 5,
		totalRecords: 0,
		currentPage: 1,
		pageSize: 10,
		hasNext: true,
		hasPrev: false,
		sortColumn: "score",
		asc: true,
	};

	sortIndex = 0;

	onChangePagePlayerStats(currentPage: number): void {
		console.log(currentPage);

		this.tablePlayerStats.currentPage = currentPage;
		this.loadPlayerStats();
	}

	onSortTeamStats($event: any): void {
		switch ($event) {
			case 0: {
				if (this.tableTeamStats.sortColumn === "score") {
					this.tableTeamStats.asc = !this.tableTeamStats.asc;
				}
				this.tableTeamStats.sortColumn = "score";
				this.sortIndex = 0;
				break;
			}
			case 1: {
				if (this.tableTeamStats.sortColumn === "yellowcard") {
					this.tableTeamStats.asc = !this.tableTeamStats.asc;
				}
				this.tableTeamStats.sortColumn = "yellowcard";
				this.sortIndex = 1;
				break;
			}
			case 2: {
				if (this.tableTeamStats.sortColumn === "redcard") {
					this.tableTeamStats.asc = !this.tableTeamStats.asc;
				}
				this.tableTeamStats.sortColumn = "redcard";
				this.sortIndex = 2;
				break;
			}
		}
		this.loadTeamStats();
	}
	onSortPlayerStats($event: any): void {
		switch ($event) {
			case 0: {
				if (this.tablePlayerStats.sortColumn === "score") {
					this.tablePlayerStats.asc = !this.tablePlayerStats.asc;
				}
				this.tablePlayerStats.sortColumn = "score";
				this.sortIndex = 0;
				break;
			}
			case 1: {
				if (this.tablePlayerStats.sortColumn === "yellowcard") {
					this.tablePlayerStats.asc = !this.tablePlayerStats.asc;
				}
				this.tablePlayerStats.sortColumn = "yellowcard";
				this.sortIndex = 1;
				break;
			}
			case 2: {
				if (this.tablePlayerStats.sortColumn === "redcard") {
					this.tablePlayerStats.asc = !this.tablePlayerStats.asc;
				}
				this.tablePlayerStats.sortColumn = "redcard";
				this.sortIndex = 2;
				break;
			}
		}
		this.loadPlayerStats();
	}

	arrayTeam: Array<any>;

	loadTeamStats(): void {
		// * Team Stats
		this.playerStatsService
			.getTeamStatsByIdTournament(
				this.Tour.id,
				this.tableTeamStats.currentPage,
				this.tableTeamStats.pageSize,
				this.tableTeamStats.sortColumn,
				this.tableTeamStats.asc
			)
			.subscribe({
				next: (res) => {
					const value = Object.values(res);

					this.arrayTeam = value[0] as Array<any>;

					const observables = this.arrayTeam.map((element) => {
						const idTeam = element.idteam;
						return this.teamService.getById(idTeam).pipe(
							map((team) => ({
								team: team,
								stats: element,
							}))
						);
					});

					forkJoin(observables).subscribe((results) => {
						this.arrayTeam = results;
						this.teamStatsSource = results;
						this.teamStats = this.arrayTeam;
					});
					this.loadPlayerStats();
				},
			});
	}

	loadPlayerStats(): void {
		this.playerStatsService
			.getPlayerStatsByIdTournament(
				this.Tour.id,
				this.tablePlayerStats.currentPage,
				this.tablePlayerStats.pageSize,
				this.tablePlayerStats.sortColumn,
				this.tablePlayerStats.asc
			)
			.subscribe({
				next: (res) => {
					const value = Object.values(res);

					let array = value[0] as Array<any>;
					this.tablePlayerStats.currentPage = value[1] as number;
					this.tablePlayerStats.pageSize = value[2] as number;
					this.tablePlayerStats.asc = value[3] as boolean;
					this.tablePlayerStats.totalPage = value[4] as number;
					this.tablePlayerStats.totalRecords = value[5] as number;
					this.tablePlayerStats.hasPrev = value[6] as boolean;
					this.tablePlayerStats.hasNext = value[7] as boolean;

					const observables = array.map((element) => {
						const playerStatsId = element.playerStatsId;
						return this.playerService.getById(playerStatsId).pipe(
							map((player) => {
								const teamObject = this.arrayTeam.find((x) => {
									return x.team.id == player.teamId;
								});

								return {
									team: teamObject.team,
									player: player,
									stats: element,
								};
							})
						);
					});

					forkJoin(observables).subscribe((result) => {
						array = result;
						let max = 0;
						array.forEach((element) => {
							if (element.stats.score > max) {
								max = element.stats.score;
								this.topScore = element;
							}
						});
						this.playerStats = array;
					});
				},
			});
	}
}

export const dataTeam = [
	{
		id: 1,
		name: "Manchester City",
		score: 20,
		assits: 20,
		yellowCards: 4,
		redCards: 0,
	},
	{
		id: 2,
		name: "Manchester United",
		score: 15,
		assits: 15,
		yellowCards: 6,
		redCards: 1,
	},
	{
		id: 3,
		name: "Chelsea FC",
		score: 25,
		assits: 25,
		yellowCards: 5,
		redCards: 0,
	},
	{
		id: 4,
		name: "Liverpool",
		score: 30,
		assits: 30,
		yellowCards: 6,
		redCards: 1,
	},
];
