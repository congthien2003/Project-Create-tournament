import {
	Component,
	Input,
	OnChanges,
	OnInit,
	SimpleChanges,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { IdloaderService } from "src/app/shared/services/idloader.service";

@Component({
	selector: "app-navbar",
	templateUrl: "./navbar.component.html",
	styleUrls: ["./navbar.component.scss"],
})
export class NavbarComponent implements OnInit, OnChanges {
	idTour: number = 0;

	currentId: string | null = null;
	private subscription!: Subscription;

	links = [
		{
			href: "overview",
			name: "Tổng quan",
		},
		{
			href: "bracket",
			name: "Bảng đấu",
		},
		{
			href: "team",
			name: "Đội thi đấu",
		},
		{
			href: "stats",
			name: "Thống kê",
		},
	];
	activeLink: string;

	constructor(
		private idloaderService: IdloaderService,
		private router: Router
	) {}
	ngOnChanges(changes: SimpleChanges): void {
		this.activeRoute();
	}

	ngOnInit(): void {
		this.subscription = this.idloaderService.currentId$.subscribe({
			next: (id) => (this.idTour = id ?? 0),
		});
		this.activeLink = this.links[0].href;
	}

	ngOnDestroy() {
		this.subscription.unsubscribe();
	}

	activeRoute(): void {
		const url = this.router.url;

		this.links.forEach((link) => {
			if (url.includes(link.href)) {
				const temp = link.href.toString();
				console.log(temp);
			}
		});
	}
}
