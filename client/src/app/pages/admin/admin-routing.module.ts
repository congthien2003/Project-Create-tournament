import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { authGuard } from "src/app/@auth/guards/auth.guard";
import { roleGuard } from "src/app/@auth/guards/role.guard";
import { UserComponent } from "../user/user.component";
import { AdminComponent } from "./admin.component";
import { ManagementUserComponent } from "./management-user/management-user.component";
import { ManagementTournamentComponent } from "./management-tournament/management-tournament.component";
import { ManagementFormatTypeComponent } from "./management-format-type/management-format-type.component";
import { ManagementSportTypeComponent } from "./management-sport-type/management-sport-type.component";
import { adminGuard } from "src/app/@auth/guards/admin.guard";

const routes: Routes = [
	{ path: "admin", redirectTo: "admin/dashboard", pathMatch: "full" },
	{
		path: "admin",
		component: AdminComponent,
		canActivate: [authGuard, adminGuard],
		children: [
			{
				path: "dashboard",
				component: DashboardComponent,
			},
			{
				path: "user",
				component: ManagementUserComponent,
			},
			{
				path: "tournament",
				component: ManagementTournamentComponent,
			},
			{
				path: "formattype",
				component: ManagementFormatTypeComponent,
			},
			{
				path: "sporttype",
				component: ManagementSportTypeComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AdminRoutingModule {}
