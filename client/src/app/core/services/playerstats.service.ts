import { Injectable } from "@angular/core";
import { PlayerStatsApi } from "../constant/api/playerstats.api";
import { MasterService } from "./master/master.service";
import { PlayerStats } from "../models/classes/PlayerStats";
import { Observable } from "rxjs";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class PlayerStatsService {
	endpoints = PlayerStatsApi;
	constructor(private master: MasterService) {}

	create(data: PlayerStats): Observable<PlayerStats> {
		return this.master.get(this.endpoints.getById, data);
	}

	// Get by idMatch & idPlayer
	getByIdMatchIdPlayer(
		idMatch: number,
		idPlayer: number
	): Observable<PlayerStats> {
		const params = new HttpParams()
			.set("idMatch", idMatch)
			.set("idPlayer", idPlayer);
		return this.master.get(this.endpoints.getById, { params });
	}

	// Get by idMatch & idPlayer
	updateById(id: number, data: PlayerStats): Observable<PlayerStats> {
		return this.master.put(`${this.endpoints.getById}/${id}`, data);
	}

	// Get by idStats
	getByIdStats(id: number): Observable<PlayerStats> {
		return this.master.get(`${this.endpoints.getById}/${id}`);
	}

	// Get by id MatchResult
	getByIdMatchResult(idMatchResult: number): Observable<PlayerStats> {
		const params = new HttpParams().set("idMatchResult", idMatchResult);
		return this.master.get(`${this.endpoints.getByIdMatchResult}`, {
			params,
		});
	}

	// Get by idPlayer
	getByIdPlayer(idPlayer: number): Observable<PlayerStats> {
		const params = new HttpParams().set("idPlayer", idPlayer);
		return this.master.get(`${this.endpoints.getByIdPlayer}`, {
			params,
		});
	}

	// Get by idTour
	getByIdTournament(idTournament: number): Observable<PlayerStats[]> {
		const params = new HttpParams().set("idTournament", idTournament);
		return this.master.get(`${this.endpoints.getByIdTour}`, {
			params,
		});
	}

	// Get Score by idTour
	getScoreByIdTournament(idTournament: number): Observable<PlayerStats> {
		const params = new HttpParams().set("idTournament", idTournament);
		return this.master.get(`${this.endpoints.getScoreByIdTour}`, {
			params,
		});
	}

	// Get top score by idTour
	getTopScoreByIdTournament(
		idTournament: number,
		currentPage: number = 1,
		pageSize: number = 10,
		sortColumn: string = "",
		ascendingOrder: boolean = false
	): Observable<any> {
		const params = new HttpParams()
			.set("idTournament", idTournament)
			.set("currentPage", currentPage)
			.set("pageSize", pageSize)
			.set("sortColumn", sortColumn)
			.set("ascendingOrder", ascendingOrder);
		return this.master.get(`${this.endpoints.getTopListByIdTour}`, {
			params,
		});
	}
}
