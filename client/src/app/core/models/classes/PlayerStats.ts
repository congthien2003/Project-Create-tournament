import { IPlayerStats } from "../interfaces/IPlayerStats";

export class PlayerStats implements IPlayerStats {
	id: number;
	yellowCard: number;
	redCard: number;
	score: number;
	assits: number;
	playerId: number;
	matchResultId: number;

	constructor() {
		this.yellowCard = 0;
		this.redCard = 0;
		this.score = 0;
		this.assits = 0;
	}
}
