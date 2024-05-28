import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { Subscription, forkJoin, map } from "rxjs";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { MatchResultService } from "src/app/core/services/match-result.service";
import { MatchService } from "src/app/core/services/match.service";
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

	isAuthenticated: boolean = false;
	canEdit: boolean = false;

	private subscription: Subscription;

	constructor(
		private tourService: TournamentService,
		private authService: AuthenticationService,
		private matchService: MatchService,
		private matchResultService: MatchResultService,

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
			},
			error: (error) => {},
		});
	}
}
