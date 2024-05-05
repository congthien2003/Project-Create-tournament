import { Component, OnInit } from "@angular/core";
import { User } from "src/app/core/models/classes/User";
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from "@angular/forms";
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
	loginForm = new FormGroup({
		username: new FormControl<string>("", [Validators.required]),
		password: new FormControl<string>("", [Validators.required]),
	});
	constructor(
		private service: AuthenticationService,
		private toastr: ToastrService,
		private route: Router,
		private loaderService: LoaderService
	) {}

	ngOnInit() {
		this.loaderService.setLoading(true);
		this.initForm();
		setTimeout(() => {
			this.loaderService.setLoading(false);
		}, 500);
	}

	initForm() {
		this.loginForm.setValue({ username: "", password: "" });
	}

	login() {
		this.loaderService.setLoading(true);
		const username = this.loginForm.get("username")?.value;
		const password = this.loginForm.get("password")?.value;

		if (this.service.isAuthenticated()) {
			this.route.navigate(["/pages"]);
		} else {
			this.service
				.login(username ? username : "", password ? password : "")
				.subscribe({
					next: (data) => {
						this.loaderService.setLoading(false);

						localStorage.setItem("token", data.token);
						this.route.navigate(["/pages"]);
					},
					error: (error) => {
						this.toastr.error("", "Đăng nhập không thành công !", {
							timeOut: 3000,
						});
						this.loaderService.setLoading(false);
						this.initForm();
					},
				});
		}
		this.loaderService.setLoading(false);
	}
}
