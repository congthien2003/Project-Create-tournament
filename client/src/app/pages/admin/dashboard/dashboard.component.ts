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
	countTour: number = 0;
	countUser: number = 0;
	constructor(
		private tourService: TournamentService,
		private userService: UserService
	) {}
	ngOnInit(): void {
		this.tourService.getCount().subscribe({
			next: (res) => {
				const value = Object.values(res);
				this.countTour = value[4] as number;
			},
		});

		this.userService.getCount().subscribe({
			next: (value) => {
				this.countUser = value;
			},
		});
	}
}
