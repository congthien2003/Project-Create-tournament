import { Injectable } from "@angular/core";
import { MasterService } from "./master/master.service";
import { TeamApi } from "../constant/api/team.api";
import { Observable } from "rxjs";
import { Team } from "../models/classes/Team";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class TeamService {
	endpoints = TeamApi;
	constructor(private master: MasterService) {}

	getAll(idTournament: number): Observable<Team[]> {
		const params = new HttpParams().set("idTournament", idTournament);
		return this.master.get(`${this.endpoints.getAll}`, { params });
	}

	getById(idTeam: number): Observable<Team> {
		return this.master.get(`${this.endpoints.getById}/${idTeam}`);
	}

	getListTeamSwap(idTournament: number, round: number): Observable<Team[]> {
		const params = new HttpParams()
			.set("idTournament", idTournament)
			.set("round", round);
		return this.master.get(`${this.endpoints.getListTeamSwap}`, { params });
	}

	create(quantity: number, idTournament: number): Observable<Team[]> {
		const params = new HttpParams()
			.set("quantity", quantity)
			.set("idTournament", idTournament);

		return this.master.post(`${this.endpoints.create}`, null, { params });
	}

	update(id: number, name: string): Observable<Team> {
		const params = new HttpParams()
			.set("id", id)
			.set("name", name.toString());
		return this.master.put(`${this.endpoints.update}`, null, { params });
	}

	updateImage(id: number, path: string): Observable<Team> {
		const params = new HttpParams().set("id", id).set("path", path);
		return this.master.put(`${this.endpoints.updateImg}`, null, { params });
	}
}
