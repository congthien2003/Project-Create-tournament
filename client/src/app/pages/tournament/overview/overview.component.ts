import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
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

@Component({
	selector: "app-overview",
	templateUrl: "./overview.component.html",
	styleUrls: ["./overview.component.scss"],
})
export class OverviewComponent implements OnInit {
	idTour: number = 0;
	Tour: Tournament = new Tournament();
	listTeams: Team[] = [];

	isAuthenticated: boolean = false;
	canEdit: boolean = false;

	constructor(
		private route: ActivatedRoute,
		private tourService: TournamentService,
		private teamService: TeamService,
		private authService: AuthenticationService,
		public dialog: MatDialog,
		private toastr: ToastrService
	) {}

	ngOnInit(): void {
		const id = this.route.snapshot.paramMap.get("id");

		this.idTour = id ? Number.parseInt(id) : 0;

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
			error: (error) => {
				console.log(error);
			},
		});
	}

	editTour(): void {}

	saveTourData(event: any): void {
		this.Tour.name = event.name;
		this.Tour.location = event.location;

		this.tourService.updateById(this.Tour).subscribe({
			next: (tour) => {
				console.log(tour);
				this.toastr.success("", "Cập nhật thành công", {
					timeOut: 3000,
				});
			},
			error: (error) => {
				console.log(error);
				this.toastr.error("", "Cập nhật không thành công", {
					timeOut: 3000,
				});
			},
		});
	}
}
