import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";
import { ChangePasswordComponent } from "./components/change-password/change-password.component";
import { AuthRoutingModule } from "./auth-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RestorePasswordComponent } from "./components/restore-password/restore-password.component";
import { JwtModule, JwtHelperService } from "@auth0/angular-jwt";

import { SharedModule } from "../shared/shared.module";
@NgModule({
	declarations: [
		LoginComponent,
		RegisterComponent,
		ChangePasswordComponent,
		RestorePasswordComponent,
	],
	imports: [
		CommonModule,
		SharedModule,
		AuthRoutingModule,
		FormsModule,
		ReactiveFormsModule,
	],
	providers: [],
})
export class AuthModule {}
