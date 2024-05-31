import { Component, OnInit } from "@angular/core";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { TournamentService } from "src/app/core/services/tournament.service";
import { UserService } from "src/app/core/services/user.service";

@Component({
	selector: "app-dashboard",
	templateUrl: "./dashboard.component.html",
	styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
	listTour: Tournament[] = [];

	constructor(
		private tourService: TournamentService,
		private userService: UserService
	) {}
	ngOnInit(): void {
		this.tourService.getAll().subscribe({
			next: (value) => {
				this.listTour = value;
			},
		});
	}
}
