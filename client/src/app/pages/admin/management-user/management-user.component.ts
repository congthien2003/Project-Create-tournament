import { Component, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { User } from "src/app/core/models/classes/User";
import { UserService } from "src/app/core/services/user.service";

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
	constructor(private userService: UserService) {}
	ngOnInit(): void {
		this.userService.list().subscribe({
			next: (value) => {
				this.listUser = value;
			},
		});
	}
}
