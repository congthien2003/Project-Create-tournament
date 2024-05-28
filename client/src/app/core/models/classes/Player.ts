import { IPlayer } from "../interfaces/IPlayer";

export class Player implements IPlayer {
	id: number;
	name: string;
	imagePlayer?: string | undefined;
	teamId: number;

	constructor() {
		this.id = 0;
		this.name = "";
		this.imagePlayer = "";
		this.teamId = 0;
	}
}
