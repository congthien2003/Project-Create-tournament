import { Component, ElementRef, Input, OnInit, ViewChild } from "@angular/core";
import { LoaderService } from "src/app/shared/services/loader.service";
import { MatTableDataSource } from "@angular/material/table";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { TournamentService } from "src/app/core/services/tournament.service";
import { FormatTypeData } from "src/app/core/constant/data/format.data";
import { SportTypeData } from "src/app/core/constant/data/sport.data";
import { Router } from "@angular/router";
@Component({
	selector: "app-home",
	templateUrl: "./home.component.html",
	styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
	formatData = FormatTypeData;
	sportData = SportTypeData;
	constructor(
		private tournamentService: TournamentService,
		private router: Router
	) {}

	ngOnInit(): void {
		// Get Tour
		this.tournamentService.getAll(1, 5, "", "view").subscribe({
			next: (data) => {
				const value = Object.values(data);
				this.data = value[0] as Tournament[];

				this.dataSource = new MatTableDataSource<Tournament>(this.data);
			},
			error(err) {
				console.log(err);
			},
		});
	}

	ListCardInfo = [
		{
			title: "Tạo giải đấu một cách dễ dàng",
			content: "Bạn đang cần tạo một giải đấu cùng với những người bạn ?",
			classIcon: "bxs-message-dots",
		},
		{
			title: "Nhiều lựa chọn, tùy chỉnh",
			content: "Bạn đang cần tạo một giải đấu cùng với những người bạn ?",
			classIcon: "bxs-message-dots",
		},
		{
			title: "Chia sẻ cho người khác",
			content:
				"Bạn đang cần chia sẻ giải đấu với những người bạn thông qua đường link hoặc QR Code ?",
			classIcon: "bxs-message-dots",
		},
	];

	// Show Tournament

	displayedColumns: string[] = [
		"name",
		"sportType",
		"formatType",
		"quantity",
		"location",
		"startAt",
		"views",
		"actions",
	];

	data: Tournament[] = [];

	dataSource = new MatTableDataSource<Tournament>(this.data);

	@ViewChild("searchTour") search: ElementRef<HTMLInputElement>;
	onSearch(): void {
		const search = this.search.nativeElement.value;

		this.router.navigate(["/find"], {
			queryParams: { searchInput: search },
		});
	}

	onViewDetail(id: number) {
		this.router.navigateByUrl(`/tournament/${id}/overview`);
	}
}
