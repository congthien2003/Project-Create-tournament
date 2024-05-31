import { Injectable } from "@angular/core";
import { SportTypeApi } from "../constant/api/sportType.api";
import { MasterService } from "./master/master.service";
import { Observable } from "rxjs";
import { SportType } from "../models/classes/SportType";

@Injectable({
	providedIn: "root",
})
export class SportTypeService {
	endpoint = SportTypeApi;
	constructor(private master: MasterService) {}

	getAll(): Observable<SportType[]> {
		return this.master.get(this.endpoint.getAll);
	}

	getById(id: number): Observable<SportType> {
		return this.master.get(`${this.endpoint.getById}/` + id);
	}

	create(data: SportType): Observable<SportType> {
		return this.master.post(this.endpoint.create, null, data);
	}

	update(data: SportType): Observable<SportType> {
		return this.master.put(this.endpoint.update, null, data);
	}

	delteById(id: number): Observable<SportType> {
		return this.master.delete(`${this.endpoint.getById}/` + id);
	}
}
