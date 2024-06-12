import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { UserRoutingModule } from "./user-routing.module";
import { UserComponent } from "./user.component";
import { SharedModule } from "src/app/shared/shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
	declarations: [UserComponent],
	imports: [
		CommonModule,
		UserRoutingModule,
		SharedModule,
		FormsModule,
		ReactiveFormsModule,
	],
})
export class UserModule {}
