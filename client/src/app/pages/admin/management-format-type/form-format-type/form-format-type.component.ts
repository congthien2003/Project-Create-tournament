import { Component, ElementRef, Inject, ViewChild } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { SportType } from "src/app/core/models/classes/SportType";
import { FormatTypeService } from "src/app/core/services/format-type.service";
import { FormSportTypeComponent } from "../../management-sport-type/form-sport-type/form-sport-type.component";

@Component({
	selector: "app-form-format-type",
	templateUrl: "./form-format-type.component.html",
	styleUrls: ["./form-format-type.component.scss"],
})
export class FormFormatTypeComponent {
	constructor(
		private service: FormatTypeService,
		private toastr: ToastrService,
		public dialogRef: MatDialogRef<FormSportTypeComponent>,
		@Inject(MAT_DIALOG_DATA) public inputData: { id: number }
	) {
		this.isEdit = false;
		this.title = "Add new format";
	}

	isEdit: boolean;
	title: string;
	data: SportType;

	@ViewChild("input") input: ElementRef<HTMLInputElement>;
	ngOnInit(): void {
		if (this.inputData.id != 0) {
			this.isEdit = true;
			this.title = "Edit";

			this.service.getById(this.inputData.id).subscribe({
				next: (value) => {
					this.data = value;
					this.input.nativeElement.value = value.name;
				},
			});
		}
	}

	onSave(): void {
		const newName = this.input.nativeElement.value;
		this.data.name = newName;
		this.service.update(this.data).subscribe({
			next: () => {
				this.toastr.success("", "Cập nhật thành công", {
					timeOut: 3000,
				});
				this.dialogRef.close();
			},
			error: () => {
				this.toastr.error("", "Cập nhật không thành công", {
					timeOut: 3000,
				});
			},
		});
	}

	onAdd(): void {
		const newSportName = new SportType();
		newSportName.name = this.input.nativeElement.value;
		this.service.create(newSportName).subscribe({
			next: () => {
				this.toastr.success("", "Tạo thành công", {
					timeOut: 3000,
				});
				this.dialogRef.close();
			},
			error: () => {
				this.toastr.error("", "Tạo không thành công", {
					timeOut: 3000,
				});
			},
		});
	}

	onClose(): void {
		this.dialogRef.close();
	}
}
