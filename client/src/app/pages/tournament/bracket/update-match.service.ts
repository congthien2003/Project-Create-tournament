import { Injectable } from "@angular/core";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { MatchService } from "src/app/core/services/match.service";
import { BracketType } from "./bracket.component";

@Injectable({
	providedIn: "root",
})
export class UpdateMatchService {
	constructor(private matchService: MatchService) {}

	updateQuaterFinals(
		data: MatchResult,
		round16: BracketType,
		quarterfinals: BracketType
	): void {
		const matchResult = Object.create({ data: data });

		if (matchResult.idMatch == round16.matchs[0].id) {
			quarterfinals.matchs[0].idTeam1 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[1].id) {
			quarterfinals.matchs[0].idTeam2 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[2].id) {
			quarterfinals.matchs[1].idTeam1 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[3].id) {
			quarterfinals.matchs[1].idTeam2 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[4].id) {
			quarterfinals.matchs[2].idTeam2 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[5].id) {
			quarterfinals.matchs[2].idTeam2 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[6].id) {
			quarterfinals.matchs[3].idTeam2 = matchResult.idTeamWin;
		} else if (matchResult.idMatch == round16.matchs[7].id) {
			quarterfinals.matchs[3].idTeam2 = matchResult.idTeamWin;
		}
	}
}
