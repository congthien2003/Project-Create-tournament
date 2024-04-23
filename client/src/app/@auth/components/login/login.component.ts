import { Component, OnInit } from "@angular/core";
import { User } from "src/app/core/models/classes/User";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { ToastrService } from "ngx-toastr";
import { animate, style, transition, trigger } from "@angular/animations";
import { Route, Router } from "@angular/router";
import { LoaderService } from "src/app/shared/services/loader.service";

@Component({
	selector: "app-login",
	templateUrl: "./login.component.html",
	styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
	user: User = new User();
	constructor(
		private service: AuthenticationService,
		private toastr: ToastrService,
		private route: Router,
		private loaderService: LoaderService
	) {}

	ngOnInit() {
		this.loaderService.setLoading(true);

		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 500);
	}

	initForm() {}

	login() {
		this.loaderService.setLoading(true);

		if (this.service.isAuthenticated()) {
			this.route.navigate(["/pages"]);
		} else {
			this.service.login(this.user.email, this.user.password).subscribe({
				next: (data) => {
					this.loaderService.setLoading(false);

					localStorage.setItem("token", data.token);
					this.route.navigate(["/pages"]);
				},
				error: (error) => {
					this.toastr.error(
						error.error.errors[0],
						"Đăng nhập không thành công !",
						{
							timeOut: 3000,
						}
					);

					this.loaderService.setLoading(false);
				},
			});
		}
		this.loaderService.setLoading(false);
	}
}
