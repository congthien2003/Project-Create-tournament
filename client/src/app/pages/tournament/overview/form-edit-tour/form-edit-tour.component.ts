import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from "@angular/forms";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { TournamentService } from "src/app/core/services/tournament.service";

@Component({
	selector: "form-edit-tour",
	templateUrl: "./form-edit-tour.component.html",
	styleUrls: ["./form-edit-tour.component.scss"],
})
export class FormEditTourComponent implements OnInit {
	currentTour = new Tournament();
	editTour = new FormGroup({
		name: new FormControl<string>("", [
			Validators.minLength(3),
			Validators.required,
		]),
		location: new FormControl<string>("", [Validators.required]),
	});
	@Input() idTour: number = 0;

	@Output() save = new EventEmitter<any>();

	constructor(private tourService: TournamentService) {}

	ngOnInit(): void {
		this.loadTour();
	}

	loadTour(): void {
		this.tourService.getById(this.idTour).subscribe({
			next: (tour) => {
				this.currentTour = tour;
				this.editTour.setValue({
					name: this.currentTour.name,
					location: this.currentTour.location,
				});
			},
			error: (error) => {
				console.log(error);
			},
		});
	}

	handleSave(): void {
		this.save.emit(this.editTour.value);
	}
}
