import { CanActivateFn } from "@angular/router";
import { ROLES } from "../roles";
import { inject } from "@angular/core";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
export const roleGuard: CanActivateFn = (route, state) => {
	const authService = inject(AuthenticationService);
	const role = authService.getUserRoleFromToken();
	if (role === 0) {
		return true;
	} else {
		return false;
	}
};
