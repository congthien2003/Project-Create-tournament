import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { PagesRoutingModule } from "./pages-routing.module";
import { HomeComponent } from "./home/home.component";
import { PagesComponent } from "./pages.component";
import { CreateTournamentComponent } from "./create-tournament/create-tournament.component";
import { ReactiveFormsModule } from "@angular/forms";
import { CardInfoComponent } from "../shared/components/card-info/card-info.component";
import { SharedModule } from "../shared/shared.module";
import { MatCommonModule } from "@angular/material/core";

import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { FindTournamentComponent } from "./find-tournament/find-tournament.component";
import { TournamentModule } from "./tournament/tournament.module";
import { MatMenuModule } from "@angular/material/menu";
import { MytourComponent } from "./mytour/mytour.component";
import { UserComponent } from "./user/user.component";
const MatModule = [
	MatCommonModule,
	MatTableModule,
	MatPaginatorModule,
	MatMenuModule,
];

@NgModule({
	declarations: [
		HomeComponent,
		PagesComponent,
		CreateTournamentComponent,
		CardInfoComponent,
		FindTournamentComponent,
		MytourComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		PagesRoutingModule,
		ReactiveFormsModule,
		TournamentModule,
		...MatModule,
	],
})
export class PagesModule {}
