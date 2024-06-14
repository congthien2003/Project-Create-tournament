import { Component, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { FormatType } from "src/app/core/models/classes/FormatType";
import { FormatTypeService } from "src/app/core/services/format-type.service";
import { ModalDeleteComponent } from "../components/modal-delete/modal-delete.component";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { SportType } from "src/app/core/models/classes/SportType";
import { FormFormatTypeComponent } from "./form-format-type/form-format-type.component";

@Component({
	selector: "app-management-format-type",
	templateUrl: "./management-format-type.component.html",
	styleUrls: ["./management-format-type.component.scss"],
})
export class ManagementFormatTypeComponent {
	list: FormatType[] = [];

	displayedColumns: string[] = ["id", "name", "actions"];

	data: SportType[] = [];

	dataSource = new MatTableDataSource<SportType>();

	@ViewChild(MatPaginator) paginator: MatPaginator;

	ngAfterViewInit() {
		this.dataSource.paginator = this.paginator;
	}

	constructor(
		private service: FormatTypeService,
		public dialog: MatDialog,
		private toastr: ToastrService
	) {}
	ngOnInit(): void {
		this.service.getAll().subscribe({
			next: (value) => {
				this.list = value;
				this.dataSource = new MatTableDataSource<SportType>(this.list);
			},
		});
	}

	openEditDialog(id: number): void {
		const dialogRef = this.dialog.open(FormFormatTypeComponent, {
			data: { id: id, type: FormatType, service: FormatTypeService },
		});

		dialogRef.afterClosed().subscribe((result) => {
			this.service.getAll().subscribe({
				next: (value) => {
					this.list = value;
					this.dataSource = new MatTableDataSource<SportType>(
						this.list
					);
				},
			});
		});
	}

	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id, type: FormatType, service: FormatTypeService },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.service.delteById(id).subscribe({
					next: (value) => {
						this.toastr.success("Success", "Deleted item.", {
							timeOut: 3000,
						});
						this.service.getAll().subscribe({
							next: (value) => {
								this.list = value;
								this.dataSource =
									new MatTableDataSource<SportType>(
										this.list
									);
							},
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
