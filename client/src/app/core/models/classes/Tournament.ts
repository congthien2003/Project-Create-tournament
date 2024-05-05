import { ITournament } from "../interfaces/ITournament";

export class Tournament implements ITournament {
	id: number;
	name: string;
	created: Date;
	quantityTeam: number;
	location: string;
	userId: number;
	formatTypeId: number;
	sportTypeId: number;

	constructor() {
		this.id = 0;
		this.name = "";
		this.created = new Date();
		this.quantityTeam = 0;
		this.location = "";
		this.userId = 0;
		this.formatTypeId = 0;
		this.sportTypeId = 0;
	}
}
