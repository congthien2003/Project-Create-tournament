import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { TournamentComponent } from "./tournament.component";
import { OverviewComponent } from "./overview/overview.component";
import { MytourComponent } from "../mytour/mytour.component";
import { TeamComponent } from "./team/team.component";
import { BracketComponent } from "./bracket/bracket.component";
import { StatsComponent } from "./stats/stats.component";

const routes: Routes = [
	{
		path: ":id",
		component: TournamentComponent,
		children: [
			{
				path: "overview",
				component: OverviewComponent,
			},
			{
				path: "team",
				component: TeamComponent,
			},
			{
				path: "bracket",
				component: BracketComponent,
			},
			{
				path: "stats",
				component: StatsComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class TournamentRoutingModule {}
