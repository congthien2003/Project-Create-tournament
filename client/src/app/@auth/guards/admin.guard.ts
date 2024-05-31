import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";

export const adminGuard: CanActivateFn = (route, state) => {
	const router = inject(Router);
	const authService = inject(AuthenticationService);

	console.log(authService.getUserRoleFromToken());

	if (authService.getUserRoleFromToken() === "0") {
		return true;
	} else {
		router.navigate(["auth/login"]);
		return false;
	}
};
