import {
	Component,
	EventEmitter,
	OnChanges,
	OnDestroy,
	OnInit,
	Output,
	SimpleChanges,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Team } from "src/app/core/models/classes/Team";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { TeamService } from "src/app/core/services/team.service";
import { TournamentService } from "src/app/core/services/tournament.service";

import {
	MatDialog,
	MAT_DIALOG_DATA,
	MatDialogRef,
} from "@angular/material/dialog";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { IdloaderService } from "src/app/shared/services/idloader.service";
import { Subscription, map } from "rxjs";
import { Match } from "src/app/core/models/classes/Match";
import { MatchService } from "src/app/core/services/match.service";
import { MatchResultService } from "src/app/core/services/match-result.service";

@Component({
	selector: "app-overview",
	templateUrl: "./overview.component.html",
	styleUrls: ["./overview.component.scss"],
})
export class OverviewComponent implements OnInit, OnDestroy {
	links = ["team"];
	activeLink = this.links[0];

	idTour: number = 0;
	Tour: Tournament = new Tournament();
	listTeams: Team[] = [];
	listMatchResult: any[] = [];

	isAuthenticated: boolean = false;
	canEdit: boolean = false;

	private subscription!: Subscription;

	constructor(
		private tourService: TournamentService,
		private matchService: MatchService,
		private matchResultService: MatchResultService,
		private teamService: TeamService,
		private authService: AuthenticationService,
		public dialog: MatDialog,
		private toastr: ToastrService,
		private idloaderService: IdloaderService,
		private router: Router
	) {}

	ngOnDestroy(): void {
		this.subscription.unsubscribe();
	}

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
			},
			error: (error) => {},
		});

		this.teamService.getAll(this.idTour).subscribe({
			next: (data) => {
				this.listTeams = data;
			},
			error: (error) => {},
		});

		this.matchService.list(this.idTour).subscribe({
			next: (value) => {
				value.forEach((match, index) => {
					this.matchResultService.getByIdMatch(match.id).subscribe({
						next: (value) => {
							this.listMatchResult[index] = {
								__team1: this.listTeams.find((team) => {
									return team.id === match.idTeam1;
								}),
								__team2: this.listTeams.find((team) => {
									return team.id === match.idTeam2;
								}),
								__scoreT1: value.scoreT1,
								__scoreT2: value.scoreT2,
								__idTeamWin: value.idTeamWin,
							};
						},
					});
				});

				this.listMatchResult = this.listMatchResult.slice(0, 6);
			},
		});
	}

	editTour(): void {}

	loadTour() {
		this.tourService.getById(this.idTour).subscribe({
			next: (tour) => {
				this.Tour = tour;
			},
		});
	}

	saveTourData(event: any): void {
		this.Tour.name = event.name;
		this.Tour.location = event.location;

		this.tourService.updateById(this.Tour).subscribe({
			next: (tour) => {
				this.loadTour();
				const currentUrl = this.router.url;
				this.router
					.navigateByUrl("/", { skipLocationChange: true })
					.then(() => {
						this.router.navigate([currentUrl]);
					});

				this.toastr.success("", "Cập nhật thành công", {
					timeOut: 3000,
				});
			},
			error: (error) => {
				this.toastr.error("", "Cập nhật không thành công", {
					timeOut: 3000,
				});
			},
		});
	}

	reloadTeamData(event: any): void {
		this.teamService.getAll(this.idTour).subscribe({
			next: (data) => {
				this.listTeams = data;
			},
			error: (error) => {
				console.log(error);
			},
		});
	}
}
