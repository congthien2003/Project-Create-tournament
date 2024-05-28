import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute, Router } from "@angular/router";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { TournamentService } from "src/app/core/services/tournament.service";

@Component({
	selector: "app-mytour",
	templateUrl: "./mytour.component.html",
	styleUrls: ["./mytour.component.scss"],
})
export class MytourComponent implements OnInit, AfterViewInit {
	id: number = 0;

	@ViewChild(MatPaginator)
	paginator!: MatPaginator;

	ngAfterViewInit() {
		this.dataSource.paginator = this.paginator;
	}

	displayedColumns: string[] = [
		"name",
		"sportType",
		"formatType",
		"location",
		"action",
	];

	data: Tournament[] = [];

	dataSource = new MatTableDataSource<Tournament>(this.data);
	constructor(
		private tourService: TournamentService,
		private router: Router,
		private authService: AuthenticationService
	) {}
	ngOnInit(): void {
		this.id = this.authService.getUserIdFromToken();

		this.tourService.getList(this.id).subscribe({
			next: (value) => {
				this.data = value;
				this.dataSource.data = this.data.slice(0, 8);
				console.log(this.data);
			},
		});
	}

	detailTour(id: number): void {
		this.router.navigateByUrl(`/tournament/${id}/overview`);
	}

	deleteTour(id: any): void {
		console.log(id);
	}
}
