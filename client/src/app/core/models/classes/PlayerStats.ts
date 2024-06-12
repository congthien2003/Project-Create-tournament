import { IPlayerStats } from "../interfaces/IPlayerStats";

export class PlayerStats implements IPlayerStats {
	id: number;
	yellowCard: number;
	redCard: number;
	score: number;
	assists: number;
	playerId: number;
	matchResultId: number;

	constructor(playerId: number, matchResultId: number) {
		this.yellowCard = 0;
		this.redCard = 0;
		this.score = 0;
		this.assists = 0;
		this.playerId = playerId;
		this.matchResultId = matchResultId;
	}
}
