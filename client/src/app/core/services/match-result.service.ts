import { Injectable } from "@angular/core";
import { MatchResultApi } from "../constant/api/matchresult.api";
import { MasterService } from "./master/master.service";
import { HttpParams } from "@angular/common/http";
import { MatchResult } from "../models/classes/MatchResult";
import { Observable } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class MatchResultService {
	endpoint = MatchResultApi;

	constructor(private service: MasterService) {}

	create(result: MatchResult): Observable<MatchResult> {
		return this.service.post(this.endpoint.create, result);
	}

	// Get list by matchResult
	getById(idMatchResult: number): Observable<MatchResult> {
		const params = new HttpParams().set("id", idMatchResult);
		return this.service.get(this.endpoint.getById + "/" + idMatchResult);
	}

	// Get list by Idmatch
	getByIdMatch(idMatch: number): Observable<MatchResult> {
		const params = new HttpParams().set("id", idMatch);
		return this.service.get(this.endpoint.getById + "/idMatch/" + idMatch);
	}

	updateById(
		idMatchResult: number,
		matchResult: MatchResult
	): Observable<MatchResult> {
		return this.service.put(
			`${this.endpoint.getById}/${idMatchResult}`,
			matchResult
		);
	}
}
