import { Component, OnInit } from "@angular/core";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { User } from "src/app/core/models/classes/User";
import { TournamentService } from "src/app/core/services/tournament.service";
import { UserService } from "src/app/core/services/user.service";

@Component({
	selector: "app-dashboard",
	templateUrl: "./dashboard.component.html",
	styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
	listTour: Tournament[] = [];
	countUser: number;
	constructor(
		private tourService: TournamentService,
		private userService: UserService
	) {}
	ngOnInit(): void {
		this.tourService.getCount().subscribe({
			next: (value) => {
				this.listTour = value;
			},
		});

		this.userService.getCount().subscribe({
			next: (value) => {
				this.countUser = value;
			},
		});
	}
}
