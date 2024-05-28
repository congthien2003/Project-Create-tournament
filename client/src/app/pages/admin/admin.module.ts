import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminRoutingModule } from "./admin-routing.module";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { LayoutComponent } from "../layout/admin/layout/layout.component";
import { AdminComponent } from "./admin.component";
import { ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "src/app/shared/shared.module";

import { UserComponent } from "../../shared/components/user/user.component";

@NgModule({
	declarations: [
		LayoutComponent,
		DashboardComponent,
		UserComponent,
		AdminComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		ReactiveFormsModule,
		AdminRoutingModule,
	],
})
export class AdminModule {}
