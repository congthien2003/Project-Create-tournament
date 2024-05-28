import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./home/home.component";
import { CreateTournamentComponent } from "./create-tournament/create-tournament.component";
import { authGuard } from "../@auth/guards/auth.guard";
import { FindTournamentComponent } from "./find-tournament/find-tournament.component";
import { roleGuard } from "../@auth/guards/role.guard";

const routes: Routes = [
	{
		path: "",
		component: HomeComponent,
	},
	{
		path: "create",
		component: CreateTournamentComponent,
		canActivate: [authGuard],
	},
	{
		path: "find",
		component: FindTournamentComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class PagesRoutingModule {}
