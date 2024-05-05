import { IPlayer } from "../interfaces/IPlayer";

export class Player implements IPlayer {
	id: number;
	imagePlayer?: string | undefined;
	teamId: number;

	constructor() {
		this.id = 0;
		this.imagePlayer = "";
		this.teamId = 0;
	}
}
