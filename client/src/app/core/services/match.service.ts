import { Injectable } from "@angular/core";
import { MatchApi } from "../constant/api/match.api";
import { MasterService } from "./master/master.service";
import { Observable } from "rxjs";
import { Match } from "../models/classes/Match";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class MatchService {
	endpoint = MatchApi;
	constructor(private service: MasterService) {}

	createStart(idTournament: number) {
		const params = new HttpParams().set("idTournament", idTournament);
		return this.service.post(this.endpoint.createStart, null, { params });
	}

	// Get list by idTour
	list(idTournament: number): Observable<Match[]> {
		const params = new HttpParams().set("idTournament", idTournament);
		return this.service.get(this.endpoint.getAll, { params });
	}

	getById(id: number): Observable<Match> {
		return this.service.get(`${this.endpoint.getById}/${id}`);
	}

	create(match: Match): Observable<Match> {
		return this.service.post(`${this.endpoint.create}`, match);
	}

	update(id: number, match: Match): Observable<Match> {
		return this.service.put(`${this.endpoint.getById}/${id}`, match);
	}
}
