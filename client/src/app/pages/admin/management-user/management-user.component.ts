import { Component, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { SportType } from "src/app/core/models/classes/SportType";
import { User } from "src/app/core/models/classes/User";
import { UserService } from "src/app/core/services/user.service";
import { ModalDeleteComponent } from "../components/modal-delete/modal-delete.component";
import { MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";

@Component({
	selector: "app-management-user",
	templateUrl: "./management-user.component.html",
	styleUrls: ["./management-user.component.scss"],
})
export class ManagementUserComponent implements OnInit {
	listUser: User[] = [];
	displayedColumns: string[] = [
		"id",
		"username",
		"email",
		"phone",
		"role",
		"expand",
	];
	data: User[] = [];

	dataSource = new MatTableDataSource<User>(this.data);

	// TODO: Pagination
	totalPage: number;
	totalRecords: number;
	currentPage: number = 1;
	pageSize: number = 5;
	pageSizeArr: number[] = [10, 15, 20, 30];
	hasNext: any = true;
	hasPrev: any = false;

	constructor(
		private userService: UserService,
		public dialog: MatDialog,
		private toastr: ToastrService
	) {}
	ngOnInit(): void {
		this.loadList();
	}

	loadList(): void {
		this.userService.list(this.currentPage, this.pageSize).subscribe({
			next: (res) => {
				const value = Object.values(res);

				this.listUser = value[0] as User[];
				this.currentPage = value[1] as number;
				this.pageSize = value[2] as number;
				this.totalPage = value[3] as number;
				this.hasNext = value[5] as boolean;
				this.hasPrev = value[6] as boolean;

				this.dataSource = new MatTableDataSource<User>(this.listUser);
			},
		});
	}

	onChangePage(currentPage: number): void {
		this.currentPage = currentPage;
		this.loadList();
	}

	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.userService.deleteById(id).subscribe({
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
