import {
	Component,
	ElementRef,
	Input,
	OnChanges,
	OnInit,
	SimpleChanges,
	ViewChild,
} from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { Player } from "src/app/core/models/classes/Player";
import { Team } from "src/app/core/models/classes/Team";
import { PlayerService } from "src/app/core/services/player.service";
import { CaneditService } from "src/app/shared/services/canedit.service";

@Component({
	selector: "team-card",
	templateUrl: "./team-card.component.html",
	styleUrls: ["./team-card.component.scss"],
})
export class TeamCardComponent implements OnInit {
	constructor(
		private playerService: PlayerService,
		private toastr: ToastrService
	) {}
	ngOnInit(): void {
		this.loadPlayer();
	}

	@Input() team: Team;

	@Input() canEdit: boolean;
	listPlayer: Player[];

	playerName: string = "";
	@ViewChild("myInput") myInput: ElementRef;

	loadPlayer(): void {
		this.playerService.getAll(this.team.id).subscribe({
			next: (value) => {
				this.listPlayer = value;
			},
		});
	}

	addPlayer(inputEl: HTMLInputElement): void {
		console.log("Input value:", inputEl.value);
		if (inputEl.value.trim() === "") {
			this.toastr.warning("", "Vui lòng nhập tên thành viên", {
				timeOut: 3000,
			});
		} else {
			this.playerService
				.create(inputEl.value.trim(), this.team.id)
				.subscribe({
					next: (value) => {
						inputEl.value = "";
						this.loadPlayer();
						this.toastr.success("", "Thêm thành công", {
							timeOut: 3000,
						});
					},
				});
		}
	}

	handleDeletePlayer($event: any): void {
		console.log($event);

		this.playerService.deleteById($event).subscribe({
			next: (value) => {
				this.toastr.success("", "Xóa thành công", {
					timeOut: 3000,
				});
				this.loadPlayer();
			},
		});
	}
}
