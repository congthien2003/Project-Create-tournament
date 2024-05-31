import { Component, OnInit, ViewChild } from "@angular/core";
import { SportType } from "src/app/core/models/classes/SportType";
import { SportTypeService } from "src/app/core/services/sport-type.service";
import {
	MatDialog,
	MAT_DIALOG_DATA,
	MatDialogRef,
} from "@angular/material/dialog";
import { ModalDeleteComponent } from "../components/modal-delete/modal-delete.component";
import { ToastrService } from "ngx-toastr";
import { FormEditComponent } from "../components/form-edit/form-edit.component";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
@Component({
	selector: "app-management-sport-type",
	templateUrl: "./management-sport-type.component.html",
	styleUrls: ["./management-sport-type.component.scss"],
})
export class ManagementSportTypeComponent implements OnInit {
	list: SportType[] = [];

	displayedColumns: string[] = ["id", "name"];

	data: SportType[] = [];

	dataSource = new MatTableDataSource<SportType>();

	@ViewChild(MatPaginator) paginator: MatPaginator;

	ngAfterViewInit() {
		this.dataSource.paginator = this.paginator;
	}
	constructor(
		private service: SportTypeService,
		public dialog: MatDialog,
		private toastr: ToastrService
	) {}
	ngOnInit(): void {
		this.service.getAll().subscribe({
			next: (value) => {
				this.data = value;
				this.dataSource = new MatTableDataSource<SportType>(this.data);
			},
		});
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
				this.service.delteById(id).subscribe({
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
