import { Injectable } from "@angular/core";
import { MasterService } from "./master/master.service";
import { UserApi } from "../constant/api/user.api";
import { Observable } from "rxjs";
import { User } from "../models/classes/User";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class UserService {
	endpoint = UserApi;
	constructor(private service: MasterService) {}

	list(currentPage: number, pageSize: number): Observable<any> {
		const params = new HttpParams()
			.set("currentPage", currentPage)
			.set("pageSize", pageSize);
		return this.service.get(this.endpoint.getAll, { params });
	}

	getCount(): Observable<number> {
		return this.service.get(`${this.endpoint.getAll}/getCount`);
	}

	getById(id: number): Observable<User> {
		return this.service.get(`${this.endpoint.getById}/${id}`);
	}

	create(user: User): Observable<User> {
		return this.service.post(`${this.endpoint.create}`, user);
	}

	update(id: number, user: User): Observable<User> {
		return this.service.put(`${this.endpoint.update}/${id}`, user);
	}

	deleteById(id: number): Observable<User> {
		return this.service.delete(`${this.endpoint.delete}/${id}`);
	}
}
