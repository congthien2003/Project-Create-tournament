import {
	AfterViewInit,
	Component,
	EventEmitter,
	Input,
	OnInit,
	Output,
} from "@angular/core";
import { AngularFireStorage } from "@angular/fire/compat/storage";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Team } from "src/app/core/models/classes/Team";
import { TeamService } from "src/app/core/services/team.service";
import { FirebaseService } from "src/app/shared/services/firebase.service";

@Component({
	selector: "form-edit-team",
	templateUrl: "./form-edit-team.component.html",
	styleUrls: ["./form-edit-team.component.scss"],
})
export class FormEditTeamComponent implements AfterViewInit {
	@Input() idTour: number = 0;

	@Input() listTeams: Team[] = [];

	@Output() save = new EventEmitter<any>();

	constructor(
		private teamService: TeamService,
		private route: Router,
		private firebaseService: FirebaseService,
		private toastr: ToastrService
	) {}

	ngAfterViewInit(): void {
		this.loadAllTeam();
	}

	loadAllTeam(): void {
		this.teamService.getAll(this.idTour).subscribe({
			next: (value) => {
				this.listTeams = value;
			},
		});
	}

	handleSave(): void {
		this.listTeams.forEach((team) => {
			this.teamService.update(team.id, team.name).subscribe({
				next: (value) => {
					console.log(value);
					this.save.emit();
				},
			});
		});
	}

	teamDetail(): void {
		this.route.navigateByUrl(`tournament/${this.idTour}/team`);
	}

	onChangeFile($event: any, id: number): void {
		const team = this.listTeams.find((team) => {
			return team.id === id;
		});
		const imgUrl = team?.imageTeam ?? "";
		if (imgUrl !== "" && imgUrl !== undefined) {
			const imgRef = this.firebaseService.getImageRefFromUrl(imgUrl);
			this.firebaseService.deleteFile(imgRef);
		}

		const file = $event?.target.files[0];
		const save = this.firebaseService.saveFile("teamImg", file);
		save.then((value) => {
			this.teamService.updateImage(id, value).subscribe({
				next: (value) => {
					this.toastr.success("", "Cập nhật ảnh thành công", {
						timeOut: 3000,
					});
					this.loadAllTeam();
				},
				error: (error) => {
					this.toastr.error("", "Có lỗi xảy ra", {
						timeOut: 3000,
					});
				},
			});
		});
	}
}
