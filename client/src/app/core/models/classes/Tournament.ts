import { ITournament } from "../interfaces/ITournament";

export class Tournament implements ITournament {
	id: number;
	name: string;
	created: Date;
	finishAt: Date;
	quantityTeam: number;
	location: string;
	views: number;
	userId: number;
	formatTypeId: number;
	sportTypeId: number;
}
