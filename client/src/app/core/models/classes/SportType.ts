import { ISportType } from "../interfaces/ISportType";

export class SportType implements ISportType {
	id: number;
	name: string;

	constructor() {
		this.id = 0;
		this.name = "";
	}
}
