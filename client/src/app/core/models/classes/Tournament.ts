import { ITournament } from "../interfaces/ITournament";

export class Tournament implements ITournament {
	id: number;
	name: string;
	created: Date;
	finishAt: Date;
	quantityTeam: number;
	location: string;
	view: number;
	userId: number;
	formatTypeId: number;
	sportTypeId: number;

	constructor() {
		this.created = new Date();
		this.finishAt = new Date();
		this.quantityTeam = 0;
		this.location = "";
		this.view = 0;
	}
}
