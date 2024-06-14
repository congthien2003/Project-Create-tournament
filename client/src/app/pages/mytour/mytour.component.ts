import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute, Router } from "@angular/router";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { ModalDeleteComponent } from "./modal-delete/modal-delete.component";
import { MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";

@Component({
	selector: "app-mytour",
	templateUrl: "./mytour.component.html",
	styleUrls: ["./mytour.component.scss"],
})
export class MytourComponent implements OnInit {
	id: number = 0;

	// Pagi
	totalPage: number;
	totalRecords: number;
	currentPage: number = 1;
	pageSize: number = 9;
	pageSizeArr: number[] = [10, 15, 20, 30];
	hasNext: any = true;
	hasPrev: any = false;

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
		private authService: AuthenticationService,
		public dialog: MatDialog,
		private toastr: ToastrService
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

	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.tourService.deleteById(id).subscribe({
					next: (value) => {
						this.toastr.success("Success", "Deleted item.", {
							timeOut: 3000,
						});
					},
					error: (error) => {
						this.toastr.error("Error", "Can't delete item.", {
							timeOut: 3000,
						});
					},
				});
			}
		});
	}
}
