import {
	AfterViewInit,
	Component,
	EventEmitter,
	Input,
	OnChanges,
	OnInit,
	Output,
	SimpleChanges,
} from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Match } from "src/app/core/models/classes/Match";
import { MatchResult } from "src/app/core/models/classes/MatchResult";
import { Team } from "src/app/core/models/classes/Team";
import { MatchResultService } from "src/app/core/services/match-result.service";
import { TeamService } from "src/app/core/services/team.service";
import { FormEditResultComponent } from "./form-edit-result/form-edit-result.component";
import { FormEditInfoComponent } from "./form-edit-info/form-edit-info.component";
import { MatchService } from "src/app/core/services/match.service";
import { ToastrService } from "ngx-toastr";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { FormEditStatsComponent } from "./form-edit-stats/form-edit-stats.component";
import { ViewResultComponent } from "./view-result/view-result.component";

@Component({
	selector: "bracket-card",
	templateUrl: "./bracket-card.component.html",
	styleUrls: ["./bracket-card.component.scss", "./form-edit.scss"],
})
export class BracketCardComponent implements OnInit {
	@Input() match: Match;
	@Input() canEdit: boolean;

	@Output() saveMatch = new EventEmitter<any>();

	team1?: Team;
	team2?: Team;
	matchResult?: MatchResult;
	constructor(
		private teamService: TeamService,
		private matchService: MatchService,
		private matchResultService: MatchResultService,
		private dialogRef: MatDialog,
		private toastr: ToastrService
	) {}
	ngAfterViewInit(): void {}
	ngOnInit(): void {
		this.loadBracketCard();
	}

	loadBracketCard(): void {
		this.teamService.getById(this.match.idTeam1).subscribe({
			next: (value) => {
				this.team1 = value;
			},
			error: () => {},
		});
		this.teamService.getById(this.match.idTeam2).subscribe({
			next: (value) => {
				this.team2 = value;
			},
			error: () => {},
		});

		this.matchResultService.getByIdMatch(this.match.id).subscribe({
			next: (value) => {
				this.matchResult = value;
			},
			error: () => {
				console.log(" ");
			},
		});
	}

	openEditResult(): void {
		const dialogRef = this.dialogRef.open(FormEditResultComponent, {
			data: {
				match: this.match,
				matchResult: this.matchResult,
				team1: this.team1,
				team2: this.team2,
			},
		});
		const subscribeDialog =
			dialogRef.componentInstance.saveResult.subscribe({
				next: (data: any) => {
					this.matchResult = data.value;
					const team =
						this.matchResult?.idTeamWin === this.team1?.id
							? this.team1
							: this.team2;
					this.saveMatch.emit({
						data,
						round: this.match.round,
						team,
					});
				},
			});

		dialogRef.afterClosed().subscribe((result) => {
			subscribeDialog.unsubscribe();
		});
	}

	openEditStats(): void {
		const dialogRef = this.dialogRef.open(FormEditStatsComponent, {
			data: {
				match: this.match,
				matchResult: this.matchResult,
				team1: this.team1,
				team2: this.team2,
			},
		});
		const subscribeDialog =
			dialogRef.componentInstance.saveResult.subscribe((data) => {
				this.matchResult = data;
				this.saveMatch.emit(this.matchResult);
			});

		dialogRef.afterClosed().subscribe((result) => {
			subscribeDialog.unsubscribe();
		});
	}

	openEditMatch(): void {
		const dialogRef = this.dialogRef.open(FormEditInfoComponent, {
			data: {
				match: this.match,
				team1: this.team1,
				team2: this.team2,
			},
		});
		const subscribeDialog = dialogRef.componentInstance.saveInfo.subscribe(
			(data) => {
				this.match = data.match;
				this.match.startAt = data.date;
				this.updateMatch();
			}
		);

		dialogRef.afterClosed().subscribe((result) => {
			subscribeDialog.unsubscribe();
		});
	}

	openViewResult(): void {
		const dialogRef = this.dialogRef.open(ViewResultComponent, {
			data: {
				match: this.match,
				matchResult: this.matchResult,
				team1: this.team1,
				team2: this.team2,
			},
		});
	}

	updateMatch(): void {
		this.matchService.update(this.match.id, this.match).subscribe({
			next: (value) => {
				this.toastr.success("", "Cập nhật thành công !", {
					timeOut: 3000,
				});
				this.loadBracketCard();
			},
			error: (error) => {
				this.toastr.error(error.error, "Cập nhật không thành công !", {
					timeOut: 3000,
				});
			},
		});
	}
}
