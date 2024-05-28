import { Component } from "@angular/core";

@Component({
	selector: "app-admin",
	template: ` <admin-layout></admin-layout>
		<router-outlet></router-outlet>`,
	styleUrls: ["./admin.component.scss"],
})
export class AdminComponent {}
