import { Component, Inject, Input, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Team } from "src/app/core/models/classes/Team";

@Component({
	selector: "app-congratulations",
	templateUrl: "./congratulations.component.html",
	styleUrls: ["./congratulations.component.scss"],
})
export class CongratulationsComponent implements OnInit {
	constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
	ngOnInit(): void {
		console.log(this.data);
	}
}
