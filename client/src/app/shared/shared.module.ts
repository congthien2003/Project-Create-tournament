import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SpinnerComponent } from "./components/spinner/spinner.component";
import { DropdownComponent } from "./components/dropdown/dropdown.component";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { TourCardComponent } from "./components/tour-card/tour-card.component";
import { SportTypePipe } from "../core/utils/sportType.pipe";
import { FormatTypePipe } from "../core/utils/formatType.pipe";
import { HeaderComponent } from "../pages/layout/header/header.component";
import { FooterComponent } from "../pages/layout/footer/footer.component";
import { PagesRoutingModule } from "../pages/pages-routing.module";
@NgModule({
	declarations: [
		SpinnerComponent,
		DropdownComponent,
		TourCardComponent,
		HeaderComponent,
		FooterComponent,
		FormatTypePipe,
		SportTypePipe,
	],
	imports: [CommonModule, PagesRoutingModule],
	exports: [
		SpinnerComponent,
		DropdownComponent,
		TourCardComponent,
		FormatTypePipe,
		SportTypePipe,
		HeaderComponent,
		FooterComponent,
	],
})
export class SharedModule {}
