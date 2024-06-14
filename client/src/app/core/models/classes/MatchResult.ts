import { IMatchResult } from "../interfaces/IMatchResult";

export class MatchResult implements IMatchResult {
	id: number;
	scoreT1: number;
	scoreT2: number;
	idTeamWin: number;
	finish: Date;
	matchId: number;

	constructor() {
		this.scoreT1 = 0;
		this.scoreT2 = 0;
		this.idTeamWin = 0;
		this.matchId = 0;
		this.finish = new Date();
	}
}
