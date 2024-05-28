import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { Team } from "src/app/core/models/classes/Team";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { TeamService } from "src/app/core/services/team.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { CaneditService } from "src/app/shared/services/canedit.service";
import { IdloaderService } from "src/app/shared/services/idloader.service";

@Component({
	selector: "app-team",
	templateUrl: "./team.component.html",
	styleUrls: ["./team.component.scss"],
})
export class TeamComponent implements OnInit {
	idTour: number;
	isAuthenticated: boolean = false;
	canEdit: boolean = false;
	private subscription: Subscription;
	constructor(
		private authService: AuthenticationService,
		private idloaderService: IdloaderService,
		private canEditService: CaneditService,
		private teamService: TeamService,
		private tourService: TournamentService
	) {}

	listTeam: Team[];

	ngOnInit(): void {
		this.subscription = this.idloaderService.currentId$.subscribe({
			next: (value) => {
				this.idTour = value ?? 0;
			},
		});

		this.teamService.getAll(this.idTour).subscribe({
			next: (value) => {
				console.log(value);
				this.listTeam = value;
			},
		});

		this.tourService.getById(this.idTour).subscribe({
			next: (value) => {
				this.canEdit = this.canEditService.canEdit(value);
			},
		});
	}
}
