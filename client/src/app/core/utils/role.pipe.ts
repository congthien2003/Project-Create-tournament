import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
	name: "roleName",
})
export class RolePipe implements PipeTransform {
	transform(id: string) {
		return id == "0" ? "admin" : "user";
	}
}
