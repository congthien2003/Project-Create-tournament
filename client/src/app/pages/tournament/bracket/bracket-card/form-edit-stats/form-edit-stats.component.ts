import { Component, EventEmitter, Inject, OnInit, Output } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { forkJoin, map } from "rxjs";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { Player } from "src/app/core/models/classes/Player";
import { PlayerStats } from "src/app/core/models/classes/PlayerStats";
import { Team } from "src/app/core/models/classes/Team";
import { PlayerService } from "src/app/core/services/player.service";
import { PlayerStatsService } from "src/app/core/services/playerstats.service";
import { TeamService } from "src/app/core/services/team.service";

@Component({
	selector: "app-form-edit-stats",
	templateUrl: "./form-edit-stats.component.html",
	styleUrls: ["./form-edit-stats.component.scss", "../form-edit.scss"],
})
export class FormEditStatsComponent implements OnInit {
	listPlayerTeam1: Player[] = [];
	listPlayerTeam2: Player[] = [];

	listInputPlayerTeam1: any[] = [];
	listInputPlayerTeam2: any[] = [];

	@Output() saveResult = new EventEmitter<any>();

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: any,
		private teamService: TeamService,
		private playerSerivce: PlayerService,
		private statsService: PlayerStatsService,
		private toastr: ToastrService
	) {}

	ngOnInit(): void {
		this.playerSerivce.getAll(this.data.team1.id).subscribe({
			next: (res) => {
				this.listPlayerTeam1 = res;

				const obser = this.listPlayerTeam1.map((player) => {
					return this.statsService
						.getByIdMatchResult(this.data.matchResult.id)
						.pipe(
							map((value: any[]) => {
								let playerStats = value.find(
									(x) => x.playerId === player.id
								);

								if (playerStats === undefined) {
									playerStats = new PlayerStats(
										player.id,
										this.data.matchResult.id
									);
								}
								return {
									player,
									playerStats,
								};
							})
						);
				});

				forkJoin(obser).subscribe({
					next: (value) => {
						this.listInputPlayerTeam1 = value;
						console.log(this.listInputPlayerTeam2);
					},
				});
			},
		});

		this.playerSerivce.getAll(this.data.team2.id).subscribe({
			next: (res) => {
				this.listPlayerTeam2 = res;

				const obser = this.listPlayerTeam2.map((player) => {
					return this.statsService
						.getByIdMatchResult(this.data.matchResult.id)
						.pipe(
							map((value: any[]) => {
								let playerStats = value.find(
									(x) => x.playerId === player.id
								);

								if (playerStats === undefined) {
									playerStats = new PlayerStats(
										player.id,
										this.data.matchResult.id
									);
								}
								return {
									player,
									playerStats,
								};
							})
						);
				});

				forkJoin(obser).subscribe({
					next: (value) => {
						this.listInputPlayerTeam2 = value;
						console.log(this.listInputPlayerTeam2);
					},
				});
			},
		});
	}

	handleSave() {
		let save = 0;
		this.listInputPlayerTeam1.forEach((item) => {
			this.statsService
				.getByIdMatchIdPlayer(
					this.data.match.id,
					item.playerStats.playerId
				)
				.subscribe({
					next: (value) => {
						console.log(value);
						this.statsService
							.updateById(value.id, item.playerStats)
							.subscribe({
								next: (value) => {},
							});
					},
					error: (error) => {
						this.statsService.create(item.playerStats).subscribe({
							next: (value) => {},
							error: (error) => {},
						});
					},
				});
		});

		this.listInputPlayerTeam2.forEach((item) => {
			this.statsService
				.getByIdMatchIdPlayer(
					this.data.match.id,
					item.playerStats.playerId
				)
				.subscribe({
					next: (value) => {
						console.log("UPDATE");

						this.statsService
							.updateById(value.id, item.playerStats)
							.subscribe({
								next: (value) => {},
								error: (error) => {},
							});
					},
					error: (error) => {
						console.log("CREATE");
						console.log(error);

						this.statsService.create(item.playerStats).subscribe({
							next: (value) => {
								console.log(value);
							},
							error: (error) => {
								console.log(error);
							},
						});
					},
				});
		});

		this.toastr.success("", "Cập nhật thành công", { timeOut: 3000 });
	}
}

export const data = [
	{
		id: 0,
		yellowCard: 0,
		redCard: 0,
		score: 0,
		assits: 0,
		playerId: 0,
		matchResultId: 0,
	},
];
