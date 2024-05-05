import { Component, Input, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Team } from "src/app/core/models/classes/Team";
import { TeamService } from "src/app/core/services/team.service";
import { TournamentService } from "src/app/core/services/tournament.service";

@Component({
	selector: "form-edit-team",
	templateUrl: "./form-edit-team.component.html",
	styleUrls: ["./form-edit-team.component.scss"],
})
export class FormEditTeamComponent implements OnInit {
	@Input() idTour: number = 0;

	@Input() listTeams: Team[] = [];

	constructor(
		private tourService: TournamentService,
		private teamService: TeamService
	) {}
	ngOnInit(): void {}

	handleSave(): void {
		this.listTeams.forEach((team) => {
			console.log(team);

			this.teamService.update(team.id, team.name).subscribe({
				next(value) {
					console.log(value);
				},
			});
		});
	}
}
