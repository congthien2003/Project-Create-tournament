import { ITeam } from "../interfaces/ITeam";

export class Team implements ITeam {
	id: number;
	name: string;
	imageTeam?: string | undefined;
	point?: number | undefined;
	tournamentId: number;

	constructor() {
		this.id = 0;
		this.name = "Team";
		this.imageTeam = "";
		this.point = 0;
		this.tournamentId = 0;
	}
}
