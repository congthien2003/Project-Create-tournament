import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
	selector: "app-dropdown",
	templateUrl: "./dropdown.component.html",
	styleUrls: ["./dropdown.component.scss"],
})
export class DropdownComponent {
	constructor() {}

	@Input() title = "Select Options";
	@Input() listItem = ["Option 1", "Option 2", "Option 3"];
	@Output() outputValue: EventEmitter<number> = new EventEmitter();

	value: string = "";
	showItem: boolean = false;
	toggleListItem() {
		this.showItem = !this.showItem;
	}

	chooseItem(event: any) {
		this.value = this.listItem[event];
		this.toggleListItem();

		this.outputValue.emit(event);
	}
}
