import { Component, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { TournamentService } from "src/app/core/services/tournament.service";
import { IdloaderService } from "src/app/shared/services/idloader.service";

@Component({
	selector: "app-tournament",
	templateUrl: "./tournament.component.html",
	styleUrls: ["./tournament.component.scss"],
})
export class TournamentComponent implements OnInit, OnChanges {
	idTour: number = 0;

	tour: Tournament | undefined;

	constructor(
		private route: ActivatedRoute,
		private shared: IdloaderService,
		private tourService: TournamentService,
		private router: Router
	) {}
	ngOnChanges(changes: SimpleChanges): void {}

	ngOnInit(): void {
		const id = this.route.snapshot.paramMap.get("id");

		this.idTour = id ? Number.parseInt(id) : 0;

		this.shared.setCurrentId(this.idTour);

		this.tourService.getById(this.idTour).subscribe({
			next: (value) => {
				this.tour = value;

				this.tourService.updateView(value).subscribe({
					next: () => {},
				});
			},
		});
	}

	loadTour(): void {
		this.tourService.getById(this.idTour).subscribe({
			next: (value) => {
				this.tour = value;
			},
		});
	}
}
