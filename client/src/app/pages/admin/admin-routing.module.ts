import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { authGuard } from "src/app/@auth/guards/auth.guard";
import { roleGuard } from "src/app/@auth/guards/role.guard";
import { UserComponent } from "../../shared/components/user/user.component";
import { AdminComponent } from "./admin.component";

const routes: Routes = [
	{
		path: "",
		component: AdminComponent,
		children: [
			{
				path: "user",
				component: UserComponent,
			},
			{
				path: "dashboard",
				component: DashboardComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AdminRoutingModule {}
