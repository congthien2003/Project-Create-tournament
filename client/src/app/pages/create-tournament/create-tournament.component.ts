import { Component, OnInit } from "@angular/core";
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { FormatTypeData } from "src/app/core/constant/data/format.data";
import { SportTypeData } from "src/app/core/constant/data/sport.data";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { MatchService } from "src/app/core/services/match.service";
import { TeamService } from "src/app/core/services/team.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { LoaderService } from "src/app/shared/services/loader.service";

@Component({
	selector: "app-create-tournament",
	templateUrl: "./create-tournament.component.html",
	styleUrls: ["./create-tournament.component.scss"],
})
export class CreateTournamentComponent implements OnInit {
	newTour = new Tournament();
	createTour: FormGroup;

	selectedName = "";

	formatTypeList = FormatTypeData.listFormat;
	formatTypeNameList = FormatTypeData.listFormat.map((item) => item.nameVie);

	titleSportSelect = "Chọn môn thi đấu";
	sportTypeList = SportTypeData.listSport;
	sportTypeNameList = SportTypeData.listSport.map((item) => item.nameVie);

	titleQuantityTeams = "Số lượng đội thi đấu";
	quantityTeams = ["2", "4", "8", "16"];

	constructor(
		private loaderService: LoaderService,
		private tourService: TournamentService,
		private formBuilder: FormBuilder,
		private toastr: ToastrService,
		private teamService: TeamService,
		private mathService: MatchService,
		private authService: AuthenticationService,
		private route: Router
	) {}

	ngOnInit(): void {
		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 500);

		this.createTour = this.formBuilder.group({
			tourName: this.formBuilder.control("", [
				Validators.required,
				Validators.minLength(5),
				Validators.maxLength(30),
			]),
			format: this.formBuilder.control("", Validators.required),
		});
	}

	onChange(event: any): void {
		console.log(event.value);
	}

	onReceiveValueSportType(index: any) {
		this.newTour.sportTypeId = this.sportTypeList[index].id;
	}
	onReceiveValueQuantityTeams(index: any) {
		this.newTour.quantityTeam = Number.parseInt(this.quantityTeams[index]);
	}

	create(): void {
		this.newTour.name = this.createTour.get("tourName")?.value;
		const indexFormat = this.createTour.get("format")?.value;
		this.newTour.formatTypeId = this.formatTypeList[indexFormat]?.id;
		let Token = Object.entries(this.authService.getInfoToken());
		this.newTour.userId = Token[0][1];
		this.tourService.create(this.newTour).subscribe({
			next: (value) => {
				const id = value.id;
				this.toastr.success("", "Tạo giải đấu thành công !", {
					timeOut: 3000,
				});
				setTimeout(() => {
					this.route.navigate([`/tournament/${id}/overview`]);
				}, 1000);
			},
			error: (err) => {
				this.toastr.error("", "Có lỗi xảy ra", {
					timeOut: 3000,
				});
				this.route.navigate([`/pages/create`]);
			},
		});
	}
}
