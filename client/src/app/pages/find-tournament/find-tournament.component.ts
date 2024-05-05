import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { FormatTypeData } from "src/app/core/constant/data/format.data";
import { SportTypeData } from "src/app/core/constant/data/sport.data";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { TournamentService } from "src/app/core/services/tournament.service";
import { LoaderService } from "src/app/shared/services/loader.service";

@Component({
	selector: "app-find-tournament",
	templateUrl: "./find-tournament.component.html",
	styleUrls: ["./find-tournament.component.scss"],
})
export class FindTournamentComponent implements OnInit {
	formatTypeList = FormatTypeData.listFormat;
	formatTypeNameList = FormatTypeData.listFormat.map((item) => item.name);

	titleSportSelect = "Chọn môn thi đấu";
	sportTypeList = SportTypeData.listSport;
	sportTypeNameList = SportTypeData.listSport.map((item) => item.name);

	constructor(
		private loaderService: LoaderService,
		private tourService: TournamentService,
		private route: Router
	) {}
	listTour: Tournament[] = [];
	ngOnInit(): void {
		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 1000);

		this.tourService.getAll().subscribe({
			next: (value) => {
				this.listTour = value.slice(0, 9);
			},
		});
	}

	onReceiveValueSportType(index: any) {
		console.log(this.sportTypeList[index].id);
	}

	onViewDetail(id: number) {
		this.route.navigateByUrl(`/tournament/${id}/overview`);
	}
}
