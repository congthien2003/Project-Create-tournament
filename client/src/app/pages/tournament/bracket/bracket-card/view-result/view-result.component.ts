import { Component, EventEmitter, Inject, OnInit, Output } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { PlayerService } from "src/app/core/services/player.service";
import { PlayerStatsService } from "src/app/core/services/playerstats.service";

@Component({
	selector: "app-view-result",
	templateUrl: "./view-result.component.html",
	styleUrls: ["./view-result.component.scss"],
})
export class ViewResultComponent implements OnInit {
	listScoreTeam1: any = [];
	listScoreTeam2: any = [];

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: any,
		private stats: PlayerStatsService,
		private player: PlayerService
	) {}
	ngOnInit(): void {
		this.stats.getByIdMatchResult(this.data.matchResult.id).subscribe({
			next: (result) => {
				console.log(result);

				this.player.getAll(this.data.team1.id).subscribe({
					next: (team1) => {
						let array = result
							.filter((element) => {
								return team1.find(
									(x) =>
										x.id === element.playerId &&
										element.score > 0
								);
							})
							.map((element) => {
								const score = Array.from(
									{ length: element.score },
									(_, i) => i
								);
								const player = team1.find(
									(x) => x.id === element.playerId
								);
								return {
									score,
									player,
								};
							});
						this.listScoreTeam1 = array as Array<any>;
						console.log(this.listScoreTeam1);
					},
				});

				this.player.getAll(this.data.team2.id).subscribe({
					next: (team2) => {
						let array = result
							.filter((element) => {
								return team2.find(
									(x) =>
										x.id === element.playerId &&
										element.score > 0
								);
							})
							.map((element) => {
								const score = Array.from(
									{ length: element.score },
									(_, i) => i
								);
								const player = team2.find(
									(x) => x.id === element.playerId
								);
								return {
									score,
									player,
								};
							});
						this.listScoreTeam2 = array as Array<any>;
						console.log(this.listScoreTeam2);
					},
				});
			},
		});
	}
}
