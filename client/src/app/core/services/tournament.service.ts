import { Injectable } from "@angular/core";
import { MasterService } from "./master/master.service";
import { TournamentApi } from "../constant/api/tournament.api";
import { Observable } from "rxjs";
import { Tournament } from "../models/classes/Tournament";

@Injectable({
	providedIn: "root",
})
export class TournamentService {
	endpoints = TournamentApi;
	constructor(private master: MasterService) {}

	// Get All Tour
	getAll(): Observable<Tournament[]> {
		return this.master.get(this.endpoints.getAll);
	}

	// Get list by userId
	getList(id: number): Observable<Tournament> {
		return this.master.get(`${this.endpoints.getList}/${id}`);
	}

	getById(id: number): Observable<Tournament> {
		return this.master.get(`${this.endpoints.getById}/${id}`);
	}

	create(tournament: Tournament): Observable<Tournament> {
		return this.master.post(`${this.endpoints.create}`, tournament);
	}

	updateById(tournament: Tournament): Observable<Tournament> {
		return this.master.put(`${this.endpoints.update}`, tournament);
	}

	deleteById(id: number): Observable<Tournament> {
		return this.master.delete(`${this.endpoints.deleteById}/${id}`);
	}

	searchByName(name: string): Observable<Tournament> {
		return this.master.get(`${this.endpoints.search}`, name);
	}
}
