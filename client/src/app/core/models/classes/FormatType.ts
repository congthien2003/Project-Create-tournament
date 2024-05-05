import { IFormatType } from "../interfaces/IFormatType";

export class FormatType implements IFormatType {
	id: number;
	name: string;

	constructor() {
		this.id = 0;
		this.name = "";
	}
}
