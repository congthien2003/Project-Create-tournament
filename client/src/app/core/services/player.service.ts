import { Injectable } from "@angular/core";
import { PlayerApi } from "../constant/api/player.api";
import { MasterService } from "./master/master.service";
import { Observable } from "rxjs";
import { Player } from "../models/classes/Player";
import { HttpParams } from "@angular/common/http";

@Injectable({
	providedIn: "root",
})
export class PlayerService {
	endpoints = PlayerApi;
	constructor(private master: MasterService) {}

	getAll(id: number): Observable<Player[]> {
		const params = new HttpParams().set("idTeam", id);
		return this.master.get(`${this.endpoints.getAll}`, { params });
	}

	getById(id: number): Observable<Player> {
		return this.master.get(`${this.endpoints.ById}/${id}`);
	}

	create(name: string, idTeam: number): Observable<Player> {
		const params = new HttpParams().set("name", name).set("idTeam", idTeam);
		return this.master.post(`${this.endpoints.create}`, undefined, {
			params,
		});
	}

	updateById(id: number, player: Player): Observable<Player> {
		return this.master.put(`${this.endpoints.ById}/${id}`, player);
	}

	deleteById(id: number): Observable<Player> {
		return this.master.delete(`${this.endpoints.ById}/${id}`);
	}
}
