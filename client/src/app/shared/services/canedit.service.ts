import { Injectable } from "@angular/core";
import { Tournament } from "src/app/core/models/classes/Tournament";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";

@Injectable({
	providedIn: "root",
})
export class CaneditService {
	constructor(private authService: AuthenticationService) {}

	canEdit(tour: Tournament): boolean {
		// Can Edit
		const isAuthenticated = this.authService.isAuthenticated();
		const userId = this.authService.getUserIdFromToken();

		if (isAuthenticated === true && userId === tour.userId.toString()) {
			return true;
		} else {
			return false;
		}
	}
}
