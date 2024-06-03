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
		"assits",
		"redCard",
		"yellowCard",
	];
	dataSource = new MatTableDataSource(dataTeam);

	@ViewChild(MatSort) sort: MatSort;

	ngAfterViewInit() {
		this.dataSource.sort = this.sort;
	}

	/** Announce the change in sort state for assistive technology. */
	announceSortChange($event: any) {
		// This example uses English messages. If your application supports
		// multiple language, you would internationalize these strings.
		// Furthermore, you can customize the message to add additional
		// details about the values being sorted.
		if ($event.direction) {
			this._liveAnnouncer.announce(`Sorted ${$event.direction}ending`);
		} else {
			this._liveAnnouncer.announce("Sorting cleared");
		}
	}

	private subscription: Subscription;

	constructor(
		private tourService: TournamentService,
		private authService: AuthenticationService,
		private matchService: MatchService,
		private matchResultService: MatchResultService,
		private playerStatsService: PlayerStatsService,
		private playerService: PlayerService,
		public dialog: MatDialog,
		private toastr: ToastrService,
		private idloaderService: IdloaderService,
		private _liveAnnouncer: LiveAnnouncer
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
				this.playerStatsService
					.getByIdTournament(this.Tour.id)
					.subscribe({
						next: (value) => {
							this.listStats = value;
							const reformattedArray = this.listStats.map(
								(element) => {
									const idPlayer = element.playerId;
									this.playerService
										.getById(idPlayer)
										.subscribe({
											next: (value) => {
												return [...value.name];
											},
										});
								}
							);
							// console.log(this.listStats);
							// console.log(reformattedArray);

							// this.dataSource = new MatTableDataSource(
							// 	dataTeam
							// );
						},
					});
			},
			error: (error) => {},
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
