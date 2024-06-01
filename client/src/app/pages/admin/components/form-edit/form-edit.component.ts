import { Component, Injector, Input, Type } from "@angular/core";
import { FormatTypeService } from "src/app/core/services/format-type.service";
import { SportTypeService } from "src/app/core/services/sport-type.service";

@Component({
	selector: "app-form-edit",
	templateUrl: "./form-edit.component.html",
	styleUrls: ["./form-edit.component.scss"],
})
export class FormEditComponent {
	private injectedService: any;

	@Input() data: any;
	typeObject: number;
	constructor(private injector: Injector) {}

	ngOnInit() {
		this.injectService();

		this.injectedService.getById(this.data.id).subscribe({
			next: (value: any) => {
				console.log(value);
			},
		});
	}

	injectService() {
		const serviceType = this.getServiceType(this.data.type);
		if (serviceType) {
			this.injectedService = this.injector.get(serviceType);
		}
	}

	getServiceType(typeClass: string): Type<any> | null {
		switch (typeClass) {
			case "FormatType": {
				this.typeObject = 0;
				return FormatTypeService;
			}
			case "SportType":
				this.typeObject = 1;
				return SportTypeService;
			// Add more cases for other services
			default:
				return null;
		}
	}
}
