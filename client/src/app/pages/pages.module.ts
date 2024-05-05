import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { PagesRoutingModule } from "./pages-routing.module";
import { HeaderComponent } from "./layout/header/header.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { HomeComponent } from "./home/home.component";
import { PagesComponent } from "./pages.component";
import { CreateTournamentComponent } from "./create-tournament/create-tournament.component";
import { ReactiveFormsModule } from "@angular/forms";
import { CardInfoComponent } from "../shared/components/card-info/card-info.component";
import { SharedModule } from "../shared/shared.module";
import { UserComponent } from "./user/user.component";
import { MatCommonModule } from "@angular/material/core";

import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { FindTournamentComponent } from "./find-tournament/find-tournament.component";
import { TournamentModule } from "./tournament/tournament.module";

@NgModule({
	declarations: [
		HomeComponent,
		PagesComponent,
		UserComponent,
		CreateTournamentComponent,
		CardInfoComponent,
		FindTournamentComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		PagesRoutingModule,
		ReactiveFormsModule,
		MatCommonModule,
		MatTableModule,
		MatPaginatorModule,
		TournamentModule,
	],
})
export class PagesModule {}
