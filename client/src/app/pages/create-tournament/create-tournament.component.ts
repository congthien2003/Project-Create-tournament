import { Component, OnInit } from "@angular/core";
import { FormatTypeData } from "src/app/core/constant/data/format.data";
import { SportTypeData } from "src/app/core/constant/data/sport.data";
import { LoaderService } from "src/app/shared/services/loader.service";

@Component({
	selector: "app-create-tournament",
	templateUrl: "./create-tournament.component.html",
	styleUrls: ["./create-tournament.component.scss"],
})
export class CreateTournamentComponent implements OnInit {
	selectedName = "";

	formatTypeList = FormatTypeData.listFormat;
	formatTypeNameList = FormatTypeData.listFormat.map((item) => item.name);

	titleSportSelect = "Chọn môn thi đấu";
	sportTypeList = SportTypeData.listSport;
	sportTypeNameList = SportTypeData.listSport.map((item) => item.name);

	titleQuantityTeams = "Số lượng đội thi đấu";
	quantityTeams = ["2", "4", "8", "12"];

	constructor(private loaderService: LoaderService) {}

	ngOnInit(): void {
		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 500);
	}

	onChange(event: any): void {
		console.log(event.value);
	}

	onReceiveValueSportType(index: any) {
		console.log(this.sportTypeList[index]);
	}
	onReceiveValueQuantityTeams(index: any) {
		console.log(this.quantityTeams[index]);
	}
}
