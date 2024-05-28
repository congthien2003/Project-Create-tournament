import { Component, Input, OnInit } from "@angular/core";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { Team } from "src/app/core/models/classes/Team";
import { MatchService } from "src/app/core/services/match.service";
import { TeamService } from "src/app/core/services/team.service";

@Component({
	selector: "history-card",
	templateUrl: "./history-card.component.html",
	styleUrls: ["./history-card.component.scss"],
})
export class HistoryCardComponent implements OnInit {
	@Input() matchResult: MatchResult;

	team1: Team;
	team2: Team;

	constructor(
		private teamService: TeamService,
		private matchService: MatchService
	) {}
	ngOnInit(): void {
		this.matchService.getById(this.matchResult.matchId).subscribe({
			next: (match) => {
				this.teamService.getById(match.idTeam1).subscribe({
					next: (value) => {
						this.team1 = value;
					},
				});
				this.teamService.getById(match.idTeam2).subscribe({
					next: (value) => {
						this.team2 = value;
					},
				});
			},
		});
	}
}
