import { Injectable } from "@angular/core";
import { formatTypeApi } from "../constant/api/formatType.api";
import { FormatType } from "../models/classes/FormatType";
import { Observable } from "rxjs";
import { MasterService } from "./master/master.service";

@Injectable({
	providedIn: "root",
})
export class FormatTypeService {
	endpoint = formatTypeApi;
	constructor(private master: MasterService) {}

	getAll(): Observable<FormatType[]> {
		return this.master.get(this.endpoint.getAll);
	}

	getById(id: number): Observable<FormatType> {
		return this.master.get(`${this.endpoint.getById}/` + id);
	}

	create(data: FormatType): Observable<FormatType> {
		return this.master.post(this.endpoint.create, null, data);
	}

	update(data: FormatType): Observable<FormatType> {
		return this.master.put(this.endpoint.update, null, data);
	}

	delteById(id: number): Observable<FormatType> {
		return this.master.delete(`${this.endpoint.getById}/` + id);
	}
}
