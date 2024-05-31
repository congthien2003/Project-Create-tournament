import { Component, OnInit } from "@angular/core";
import { Route, Router } from "@angular/router";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";

@Component({
	selector: "admin-layout",
	templateUrl: "./layout.component.html",
	styleUrls: ["./layout.component.scss"],
})
export class LayoutComponent implements OnInit {
	links = [
		{
			name: "Dashboard",
			link: "/admin/dashboard",
			iconClass: "bx bxs-home",
		},
		{
			name: "Users",
			link: "/admin/user",
			iconClass: "bx bxs-user",
		},
		{
			name: "Tournaments",
			link: "/admin/tournament",
			iconClass: "bx bx-code-alt",
		},
		{
			name: "Formats Type",
			link: "/admin/formattype",
			iconClass: "bx bxl-meta",
		},
		{
			name: "Sports Type",
			link: "/admin/sporttype",
			iconClass: "bx bxl-dribbble",
		},
	];

	activeIndex: number;
	name: string;
	constructor(
		private authService: AuthenticationService,
		private router: Router
	) {}
	ngOnInit(): void {
		this.activeIndex = 0;
		this.name = this.authService.getUsernameFromToken();
	}

	logOut(): void {
		this.authService.logout();
		setTimeout(() => {
			this.router.navigate(["/auth/login"]).then(() => {
				// Sau khi chuyển hướng, chuyển hướng lại đến '/pages', sẽ gây ra reload trang
				this.router.navigate(["/pages"]);
			});
		}, 1000);
	}
}
