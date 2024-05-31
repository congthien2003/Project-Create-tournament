import { Component, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { TournamentService } from "src/app/core/services/tournament.service";

@Component({
	selector: "app-management-tournament",
	templateUrl: "./management-tournament.component.html",
	styleUrls: ["./management-tournament.component.scss"],
})
export class ManagementTournamentComponent implements OnInit {
	displayedColumns: string[] = [
		"name",
		"sportType",
		"formatType",
		"quantity",
		"location",
	];

	data: Tournament[] = [];

	dataSource = new MatTableDataSource<Tournament>(this.data);
	constructor(private tourService: TournamentService) {}
	ngOnInit(): void {
		this.tourService.getAll().subscribe({
			next: (value) => {
				this.data = value;
			},
		});
	}
}
