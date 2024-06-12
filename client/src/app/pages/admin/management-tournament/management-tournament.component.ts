import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatTableDataSource } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { SportType } from "src/app/core/models/classes/SportType";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { SportTypeService } from "src/app/core/services/sport-type.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { FormEditComponent } from "../components/form-edit/form-edit.component";
import { ModalDeleteComponent } from "../components/modal-delete/modal-delete.component";

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
		"actions",
	];

	data: Tournament[] = [];

	dataSource = new MatTableDataSource<Tournament>(this.data);

	totalPage: number;
	totalRecords: number;
	currentPage: number = 1;
	pageSize: number = 5;
	pageSizeArr: number[] = [10, 15, 20, 30];
	hasNext: any = true;
	hasPrev: any = false;

	constructor(
		private tourService: TournamentService,
		public dialog: MatDialog,
		private toastr: ToastrService
	) {}
	ngOnInit(): void {
		this.loadList();
	}
	loadList(): void {
		// this.tourService.getAll(this.currentPage, this.pageSize).subscribe({
		// 	next: (res) => {
		// 		const value = Object.values(res);
		// 		console.log(value);

		// 		this.data = value[0] as Tournament[];
		// 		this.currentPage = value[1] as number;
		// 		this.pageSize = value[2] as number;
		// 		this.totalPage = value[3] as number;
		// 		this.hasNext = value[5] as boolean;
		// 		this.hasPrev = value[6] as boolean;

		// 		this.dataSource = new MatTableDataSource<Tournament>(this.data);
		// 	},
		// });

		this.tourService.getAllNoPagi().subscribe({
			next: (res) => {
				const value = Object.values(res);
				this.data = value[0] as Tournament[];
				this.dataSource = new MatTableDataSource<Tournament>(this.data);
			},
		});
	}
	onChangePage($event: any) {
		this.currentPage = $event;
		this.loadList();
	}

	openEditDialog(id: number): void {
		const dialogRef = this.dialog.open(FormEditComponent, {
			data: { id: id, type: SportType, service: SportTypeService },
		});

		dialogRef.afterClosed().subscribe((result) => {
			console.log("Form Edit Closed");
		});
	}

	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id, type: SportType, service: SportTypeService },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.tourService.deleteById(id).subscribe({
					next: (value) => {
						this.toastr.warning("Success", "Deleted item.", {
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
