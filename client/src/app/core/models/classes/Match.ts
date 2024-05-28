import { IMatch } from "../interfaces/IMatch";

export class Match implements IMatch {
	id: number;
	idTeam1: number;
	idTeam2: number;
	created: Date;
	startAt: Date;
	tournamentId: number;

	constructor() {
		this.id = 0;
		this.idTeam1 = 0;
		this.idTeam2 = 0;
		this.created = new Date();
		this.startAt = new Date();
		this.tournamentId = 0;
	}
}
