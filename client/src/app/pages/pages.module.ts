import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { PagesRoutingModule } from "./pages-routing.module";
import { HeaderComponent } from "./layout/header/header.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { HomeComponent } from "./home/home.component";
import { PagesComponent } from "./pages.component";
import { CreateTournamentComponent } from "./create-tournament/create-tournament.component";
import { UserComponent } from "./user/user.component";
import { ReactiveFormsModule } from "@angular/forms";
import { CardInfoComponent } from "../shared/components/card-info/card-info.component";

import { SharedModule } from "../shared/shared.module";

@NgModule({
	declarations: [
		HomeComponent,
		HeaderComponent,
		FooterComponent,
		PagesComponent,
		CreateTournamentComponent,
		UserComponent,
		CardInfoComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		PagesRoutingModule,
		ReactiveFormsModule,
	],
})
export class PagesModule {}
