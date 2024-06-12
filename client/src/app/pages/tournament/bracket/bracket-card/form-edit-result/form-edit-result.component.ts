import {
	Component,
	EventEmitter,
	Inject,
	Input,
	OnChanges,
	OnInit,
	Output,
	SimpleChanges,
} from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { Team } from "src/app/core/models/classes/Team";
import { MatchResultService } from "src/app/core/services/match-result.service";
import { MatchService } from "src/app/core/services/match.service";
import { TeamService } from "src/app/core/services/team.service";

@Component({
	selector: "form-edit-result",
	templateUrl: "./form-edit-result.component.html",
	styleUrls: ["./form-edit-result.component.scss", "../form-edit.scss"],
})
export class FormEditResultComponent implements OnInit, OnChanges {
	idMatch: number = -1;
	@Output() saveResult = new EventEmitter<any>();
	match: Match;
	matchResult: MatchResult;
	team1: Team;
	team2: Team;
	teamChoosed: number = 0;

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: any,
		private matchService: MatchService,
		private matchResultService: MatchResultService,
		private teamService: TeamService,
		private toastr: ToastrService
	) {}

	editResult = new FormGroup({
		scoreTeam1: new FormControl<number>(0, [Validators.required]),
		scoreTeam2: new FormControl<number>(0, [Validators.required]),
	});

	ngOnChanges(changes: SimpleChanges): void {}
	ngOnInit(): void {
		console.log("Form Edit");

		console.log(this.data);

		this.match = this.data.match;
		this.idMatch = this.match.id;
		this.team1 = this.data.team1;
		this.team2 = this.data.team2;

		this.matchResultService.getByIdMatch(this.match.id).subscribe({
			next: (value) => {
				this.matchResult = value;
				this.teamChoosed = this.matchResult.idTeamWin;
				this.loadMatchResult(this.matchResult);
			},
			error: () => {
				console.log("error match result");
			},
		});
	}

	loadMatchResult(matchResult: MatchResult): void {
		this.editResult.setValue({
			scoreTeam1: matchResult.scoreT1 ? matchResult.scoreT1 : 0,
			scoreTeam2: matchResult.scoreT2 ? matchResult.scoreT2 : 0,
		});
	}

	handleSave() {
		const scoreT1 = this.editResult.get("scoreTeam1")?.value;
		const scoreT2 = this.editResult.get("scoreTeam2")?.value;

		if (this.matchResult) {
			this.matchResult.scoreT1 = scoreT1 ? scoreT1 : 0;
			this.matchResult.scoreT2 = scoreT2 ? scoreT2 : 0;
			this.matchResult.idTeamWin = this.teamChoosed;
			this.matchResultService
				.updateById(this.matchResult.id, this.matchResult)
				.subscribe({
					next: () => {
						this.saveResult.emit(this.matchResult);
						this.toastr.success("", "Cập nhật thành công !", {
							timeOut: 3000,
						});
					},
					error: (error) => {
						this.toastr.error(
							error.error ? error.error : "Lỗi",
							"Cập nhật không thành công !",
							{
								timeOut: 3000,
							}
						);
					},
				});
		} else {
			let newMatchResult = new MatchResult();
			newMatchResult.scoreT1 = scoreT1 ? scoreT1 : 0;
			newMatchResult.scoreT2 = scoreT2 ? scoreT2 : 0;
			newMatchResult.matchId = this.idMatch;
			newMatchResult.idTeamWin = this.teamChoosed;
			this.matchResultService.create(newMatchResult).subscribe({
				next: () => {
					this.saveResult.emit(newMatchResult);
					this.toastr.success("", "Cập nhật thành công !", {
						timeOut: 3000,
					});
				},
				error: (error) => {
					this.toastr.error(
						error.error ? error.error : "Lỗi",
						"Cập nhật không thành công !",
						{
							timeOut: 3000,
						}
					);
				},
			});
		}
	}

	chooseTeamWin(id: number) {
		this.teamChoosed = id;
	}
}
