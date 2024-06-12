import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
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
	formatTypeNameList = FormatTypeData.listFormat.map((item) => item.nameVie);

	titleSportSelect = "Chọn môn thi đấu";
	sportTypeList = SportTypeData.listSport;
	sportTypeNameList = SportTypeData.listSport.map((item) => item.nameVie);

	searchValue: string = "";

	// Pagi
	totalPage: number;
	totalRecords: number;
	currentPage: number = 1;
	pageSize: number = 9;
	pageSizeArr: number[] = [10, 15, 20, 30];
	hasNext: any = true;
	hasPrev: any = false;

	constructor(
		private tourService: TournamentService,
		private router: Router,
		private route: ActivatedRoute
	) {}
	listTour: Tournament[] = [];
	ngOnInit(): void {
		this.searchValue =
			this.route.snapshot.queryParamMap.get("searchInput") ?? "";
		this.loadList();
	}

	loadList(): void {
		this.tourService
			.getAll(this.currentPage, this.pageSize, this.searchValue)
			.subscribe({
				next: (res) => {
					const value = Object.values(res);

					this.listTour = value[0] as Tournament[];
					this.currentPage = value[1] as number;
					this.pageSize = value[2] as number;
					this.totalPage = value[3] as number;
					this.hasNext = value[5] as boolean;
					this.hasPrev = value[6] as boolean;

					console.log(this.listTour);
				},
			});
	}

	onReceiveValueSportType(index: any) {
		console.log(this.sportTypeList[index].id);
	}

	onViewDetail(id: number) {
		this.router.navigateByUrl(`/tournament/${id}/overview`);
	}

	@ViewChild("searchInput") searchInput: ElementRef<HTMLInputElement>;
	onSearch() {
		this.searchValue = this.searchInput.nativeElement.value;
		this.tourService.searchByName(this.searchValue).subscribe({
			next: (value) => {
				this.listTour = value;
			},
		});
	}

	onChangePage(currentPage: number): void {
		this.currentPage = currentPage;
		this.loadList();
	}
}
