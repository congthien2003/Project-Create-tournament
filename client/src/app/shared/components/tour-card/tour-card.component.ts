import { Component, Input } from "@angular/core";

@Component({
	selector: "tour-card",
	templateUrl: "./tour-card.component.html",
	styleUrls: ["./tour-card.component.scss"],
})
export class TourCardComponent {
	@Input() imgUrl: string =
		"https://givetour-prod.s3.amazonaws.com/UploadFiles/AvatarPhoto/303e02e1-97c5-4ff5-ae78-22b17bb958ec.png";

	@Input() tourName: string = "Tour Name Temp ";
	@Input() format: number = 1;
	@Input() sport: number = 1;
	@Input() location: string = "Vietnam";

	@Input() quantity: number = 8;
	@Input() view: number = 10;
}
