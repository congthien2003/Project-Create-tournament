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
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatTabsModule } from "@angular/material/tabs";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatMenuModule } from "@angular/material/menu";
import { RolePipe } from "../core/utils/role.pipe";
import { PaginationComponent } from "./components/pagination/pagination.component";
import { MatSortModule } from "@angular/material/sort";
import { CongratulationsComponent } from "./components/congratulations/congratulations.component";
import { MatSelectModule } from "@angular/material/select";
const MatImport = [
	MatTabsModule,
	MatTableModule,
	MatPaginatorModule,
	MatTooltipModule,
	MatButtonModule,
	MatDialogModule,
	MatInputModule,
	MatFormFieldModule,
	MatCardModule,
	MatDialogModule,
	MatDatepickerModule,
	MatNativeDateModule,
	MatMenuModule,
	MatSortModule,
	MatSelectModule,
];

@NgModule({
	declarations: [
		SpinnerComponent,
		DropdownComponent,
		TourCardComponent,
		PaginationComponent,
		HeaderComponent,
		FooterComponent,
		FormatTypePipe,
		SportTypePipe,
		RolePipe,
		CongratulationsComponent,
	],
	imports: [CommonModule, PagesRoutingModule],
	exports: [
		SpinnerComponent,
		DropdownComponent,
		TourCardComponent,
		PaginationComponent,
		FormatTypePipe,
		SportTypePipe,
		RolePipe,
		HeaderComponent,
		FooterComponent,
		...MatImport,
	],
})
export class SharedModule {}
