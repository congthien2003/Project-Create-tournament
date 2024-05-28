import { NgModule } from "@angular/core";
import { AsyncPipe, CommonModule } from "@angular/common";
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
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatButtonModule } from "@angular/material/button";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatCardModule } from "@angular/material/card";
import { NavbarComponent } from "./navbar/navbar.component";
import { TeamComponent } from "./team/team.component";
import { BracketComponent } from "./bracket/bracket.component";
import { StatsComponent } from "./stats/stats.component";
import { BracketCardComponent } from "./bracket/bracket-card/bracket-card.component";
import { FormEditResultComponent } from "./bracket/bracket-card/form-edit-result/form-edit-result.component";
import { FormEditInfoComponent } from "./bracket/bracket-card/form-edit-info/form-edit-info.component";
import { MatNativeDateModule } from "@angular/material/core";
import { TeamCardComponent } from "./team/team-card/team-card.component";
import { PlayerCardComponent } from "./team/team-card/player-card/player-card.component";
import { HistoryCardComponent } from "./stats/history-card/history-card.component";

const MatImport = [
	MatTabsModule,
	MatTableModule,
	MatPaginatorModule,
	MatTooltipModule,
	MatButtonModule,
	MatDialogModule,
	MatInputModule,
	MatFormFieldModule,
	MatCardModule,
	MatDialogModule,
	MatDatepickerModule,
	MatNativeDateModule,
];

@NgModule({
	declarations: [
		OverviewComponent,
		TournamentComponent,
		FormEditTourComponent,
		FormEditTeamComponent,
		MytourComponent,
		NavbarComponent,
		TeamComponent,
		BracketComponent,
		StatsComponent,
		BracketCardComponent,
		FormEditResultComponent,
		FormEditInfoComponent,
		TeamCardComponent,
		PlayerCardComponent,
		HistoryCardComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		TournamentRoutingModule,
		FormsModule,
		ReactiveFormsModule,
		AsyncPipe,
		[...MatImport],
	],
})
export class TournamentModule {}
