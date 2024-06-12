import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminRoutingModule } from "./admin-routing.module";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { LayoutComponent } from "../layout/admin/layout/layout.component";
import { AdminComponent } from "./admin.component";
import { ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "src/app/shared/shared.module";

import { UserComponent } from "../user/user.component";
import { FormEditComponent } from "./components/form-edit/form-edit.component";
import { ModalDeleteComponent } from "./components/modal-delete/modal-delete.component";
import { ManagementFormatTypeComponent } from "./management-format-type/management-format-type.component";
import { ManagementSportTypeComponent } from "./management-sport-type/management-sport-type.component";
import { ManagementTournamentComponent } from "./management-tournament/management-tournament.component";
import { ManagementUserComponent } from "./management-user/management-user.component";
import { FormSportTypeComponent } from "./management-sport-type/form-sport-type/form-sport-type.component";
import { FormFormatTypeComponent } from "./management-format-type/form-format-type/form-format-type.component";

@NgModule({
	declarations: [
		LayoutComponent,
		DashboardComponent,
		AdminComponent,
		ManagementUserComponent,
		ManagementTournamentComponent,
		ManagementSportTypeComponent,
		ManagementFormatTypeComponent,
		FormEditComponent,
		ModalDeleteComponent,
		FormSportTypeComponent,
		FormFormatTypeComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		ReactiveFormsModule,
		AdminRoutingModule,
	],
})
export class AdminModule {}
