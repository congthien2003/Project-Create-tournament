import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { OverviewComponent } from "./overview/overview.component";
import { TournamentComponent } from "./tournament.component";
import { SharedModule } from "src/app/shared/shared.module";
import { TournamentRoutingModule } from "./tournament-routing.module";
import { MatTabsModule } from "@angular/material/tabs";
import { MatDialogModule } from "@angular/material/dialog";
import { FormEditTourComponent } from "./overview/form-edit-tour/form-edit-tour.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormEditTeamComponent } from "./overview/form-edit-team/form-edit-team.component";
import { MytourComponent } from "./mytour/mytour.component";

@NgModule({
	declarations: [
		OverviewComponent,
		TournamentComponent,
		FormEditTourComponent,
		FormEditTeamComponent,
		MytourComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		TournamentRoutingModule,
		MatTabsModule,

		FormsModule,
		MatDialogModule,
		ReactiveFormsModule,
	],
})
export class TournamentModule {}
