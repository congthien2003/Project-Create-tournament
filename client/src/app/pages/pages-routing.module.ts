import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./home/home.component";
import { CreateTournamentComponent } from "./create-tournament/create-tournament.component";
import { authGuard } from "../@auth/guards/auth.guard";
import { FindTournamentComponent } from "./find-tournament/find-tournament.component";
import { roleGuard } from "../@auth/guards/role.guard";
import { MytourComponent } from "./mytour/mytour.component";
import { UserComponent } from "./user/user.component";

const routes: Routes = [
	{ path: "", redirectTo: "/pages", pathMatch: "full" },
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
	{
		path: "mytour/:id",
		component: MytourComponent,
		canActivate: [authGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class PagesRoutingModule {}
