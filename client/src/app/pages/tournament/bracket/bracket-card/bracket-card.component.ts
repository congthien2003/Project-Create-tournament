import {
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

@Component({
	selector: "bracket-card",
	templateUrl: "./bracket-card.component.html",
	styleUrls: ["./bracket-card.component.scss", "./form-edit.scss"],
})
export class BracketCardComponent implements OnInit, OnChanges {
	@Input() match: Match;
	@Input() canEdit: boolean;

	@Output() saveMatch = new EventEmitter<MatchResult>();

	team1?: Team = new Team();
	team2?: Team = new Team();
	matchResult?: MatchResult;
	constructor(
		private teamService: TeamService,
		private matchService: MatchService,
		private matchResultService: MatchResultService,
		private dialogRef: MatDialog,
		private toastr: ToastrService
	) {}
	ngOnChanges(changes: SimpleChanges): void {}
	ngOnInit(): void {
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
			error: () => {},
		});
	}

	openEditResult(): void {
		const dialogRef = this.dialogRef.open(FormEditResultComponent, {
			data: {
				match: this.match,
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
			},
		});
		const subscribeDialog = dialogRef.componentInstance.saveInfo.subscribe(
			(data) => {
				this.match.startAt = data.date;
				this.updateMatch();
			}
		);

		dialogRef.afterClosed().subscribe((result) => {
			subscribeDialog.unsubscribe();
		});
	}

	updateMatch(): void {
		this.matchService.update(this.match.id, this.match).subscribe({
			next: (value) => {
				this.toastr.success("", "Cập nhật thành công !", {
					timeOut: 3000,
				});
			},
		});
	}
}
