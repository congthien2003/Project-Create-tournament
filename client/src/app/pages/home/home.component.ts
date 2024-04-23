import { AfterContentInit, Component, OnInit } from "@angular/core";
import { LoaderService } from "src/app/shared/services/loader.service";

@Component({
	selector: "app-home",
	templateUrl: "./home.component.html",
	styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
	constructor(private loaderService: LoaderService) {}

	ngOnInit(): void {
		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 1000);
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
}
