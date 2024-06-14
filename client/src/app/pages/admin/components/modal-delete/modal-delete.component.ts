import { Component, EventEmitter, Inject, Input, Output } from "@angular/core";
import {
	MatDialog,
	MatDialogRef,
	MAT_DIALOG_DATA,
} from "@angular/material/dialog";

@Component({
	selector: "app-modal-delete",
	templateUrl: "./modal-delete.component.html",
	styleUrls: ["./modal-delete.component.scss"],
})
export class ModalDeleteComponent {
	@Input() id: number;

	@Output() confirmDelete = new EventEmitter<boolean>();
	constructor(public dialogRef: MatDialogRef<ModalDeleteComponent>) {}

	onNoClick(): void {
		this.dialogRef.close();
	}

	onSubmit(): void {
		this.dialogRef.close(true);
	}
}
