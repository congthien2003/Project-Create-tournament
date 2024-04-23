import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SpinnerComponent } from "./components/spinner/spinner.component";
import { DropdownComponent } from "./components/dropdown/dropdown.component";

@NgModule({
	declarations: [SpinnerComponent, DropdownComponent],
	imports: [CommonModule],
	exports: [SpinnerComponent, DropdownComponent],
})
export class SharedModule {}
