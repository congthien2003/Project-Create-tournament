import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminRoutingModule } from "./pages/admin/admin-routing.module";
import { PagesRoutingModule } from "./pages/pages-routing.module";
import { UserComponent } from "./pages/user/user.component";
import { authGuard } from "./@auth/guards/auth.guard";

const routes: Routes = [
	{
		path: "auth",
		loadChildren: () =>
			import("./@auth/auth.module").then((m) => m.AuthModule),
	},
	{
		path: "admin",
		loadChildren: () =>
			import("./pages/admin/admin.module").then((m) => m.AdminModule),
	},
	{
		path: "pages",
		loadChildren: () =>
			import("./pages/pages.module").then((m) => m.PagesModule),
	},
	{
		path: "tournament",
		loadChildren: () =>
			import("./pages/tournament/tournament.module").then(
				(m) => m.TournamentModule
			),
	},
	{
		path: "user",
		loadChildren: () =>
			import("./pages/user/user.module").then((m) => m.UserModule),
	},

	{ path: "", redirectTo: "pages", pathMatch: "full" },
	{ path: "**", redirectTo: "pages" },
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes),
		AdminRoutingModule,
		PagesRoutingModule,
	],
	exports: [RouterModule],
})
export class AppRoutingModule {}
