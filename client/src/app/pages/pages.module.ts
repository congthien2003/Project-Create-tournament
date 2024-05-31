import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { PagesRoutingModule } from "./pages-routing.module";
import { HomeComponent } from "./home/home.component";
import { PagesComponent } from "./pages.component";
import { CreateTournamentComponent } from "./create-tournament/create-tournament.component";
import { ReactiveFormsModule } from "@angular/forms";
import { CardInfoComponent } from "../shared/components/card-info/card-info.component";
import { SharedModule } from "../shared/shared.module";
import { FindTournamentComponent } from "./find-tournament/find-tournament.component";
import { TournamentModule } from "./tournament/tournament.module";
import { MatCommonModule } from "@angular/material/core";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatTableModule } from "@angular/material/table";
const MatModule = [MatCommonModule, MatTableModule, MatPaginatorModule];
@NgModule({
	declarations: [
		HomeComponent,
		PagesComponent,
		CreateTournamentComponent,
		CardInfoComponent,
		FindTournamentComponent,
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
