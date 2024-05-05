import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { FormatTypeData } from "src/app/core/constant/data/format.data";
import { SportTypeData } from "src/app/core/constant/data/sport.data";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
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
	createTour!: FormGroup;
	selectedName = "";

	formatTypeList = FormatTypeData.listFormat;
	formatTypeNameList = FormatTypeData.listFormat.map((item) => item.name);

	titleSportSelect = "Chọn môn thi đấu";
	sportTypeList = SportTypeData.listSport;
	sportTypeNameList = SportTypeData.listSport.map((item) => item.name);

	titleQuantityTeams = "Số lượng đội thi đấu";
	quantityTeams = ["2", "4", "8", "12"];

	constructor(
		private loaderService: LoaderService,
		private tourService: TournamentService,
		private formBuilder: FormBuilder,
		private toastr: ToastrService,
		private teamService: TeamService,
		private authService: AuthenticationService,
		private route: Router
	) {}

	ngOnInit(): void {
		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 500);

		this.createTour = this.formBuilder.group({
			tourName: this.formBuilder.control(""),
			format: this.formBuilder.control(""),
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
				this.teamService
					.create(value.quantityTeam, value.id)
					.subscribe({
						next: (value) => {
							console.log("Tạo team thành công");
						},
						error: (errors) => {
							console.log(errors);
						},
					});

				this.toastr.success("", "Tạo giải đấu thành công !", {
					timeOut: 3000,
				});
				this.route.navigate([`/tournament/${id}/overview`]);
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
