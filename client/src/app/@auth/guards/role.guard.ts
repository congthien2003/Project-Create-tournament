import { CanActivateFn, Router } from "@angular/router";
import { inject } from "@angular/core";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
export const roleGuard: CanActivateFn = (route, state) => {
	const router = inject(Router);

	const authService = inject(AuthenticationService);
	const role = authService.getUserRoleFromToken();

	console.log(role);

	if (role === 0) {
		return true;
	} else {
		router.navigate(["auth/login"]);
		return false;
	}
};
