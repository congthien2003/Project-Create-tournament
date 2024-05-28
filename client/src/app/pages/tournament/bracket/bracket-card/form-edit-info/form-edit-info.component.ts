import { Component, EventEmitter, Output } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
	selector: "app-form-edit-info",
	templateUrl: "./form-edit-info.component.html",
	styleUrls: ["./form-edit-info.component.scss", "../form-edit.scss"],
})
export class FormEditInfoComponent {
	@Output() saveInfo = new EventEmitter<any>();

	editMatch = new FormGroup({
		date: new FormControl<Date>(new Date(), Validators.required),
	});

	saveMatch(): void {
		const date = this.editMatch.get("date")?.value;

		this.saveInfo.emit({
			date,
		});
	}
}
