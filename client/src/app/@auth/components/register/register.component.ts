import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { User } from "src/app/core/models/classes/User";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { LoaderService } from "src/app/shared/services/loader.service";

@Component({
	selector: "app-register",
	templateUrl: "./register.component.html",
	styleUrls: ["./register.component.scss"],
})
export class RegisterComponent implements OnInit {
	user: User = new User();

	constructor(
		private service: AuthenticationService,
		private toastr: ToastrService,
		private loaderService: LoaderService
	) {}

	ngOnInit(): void {
		this.loaderService.setLoading(true);

		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 500);
	}

	regiser() {
		this.loaderService.setLoading(true);

		this.service
			.register(this.user.username, this.user.email, this.user.password)
			.subscribe({
				next: (data) => {
					this.loaderService.setLoading(false);

					localStorage.setItem("token", data.token);
					this.toastr.error("", "Đăng ký thành công !", {
						timeOut: 3000,
					});
				},
				error: (error) => {
					this.loaderService.setLoading(false);

					this.toastr.error(
						error.error.errors[0],
						"Đăng ký không thành công !",
						{
							timeOut: 3000,
						}
					);
				},
			});
	}
}
