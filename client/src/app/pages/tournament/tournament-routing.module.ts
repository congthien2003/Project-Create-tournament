import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { TournamentComponent } from "./tournament.component";
import { OverviewComponent } from "./overview/overview.component";
import { MytourComponent } from "./mytour/mytour.component";

const routes: Routes = [
	{
		path: "",
		component: TournamentComponent,
		children: [
			{
				path: ":id/overview",
				component: OverviewComponent,
			},
			{
				path: "mytour/:id",
				component: MytourComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class TournamentRoutingModule {}
