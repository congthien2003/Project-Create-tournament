import { Injectable } from "@angular/core";
import { PlayerApi } from "../constant/api/player.api";
import { MasterService } from "./master/master.service";
import { Observable } from "rxjs";
import { Player } from "../models/classes/Player";

@Injectable({
	providedIn: "root",
})
export class PlayerService {
	endpoints = PlayerApi;
	constructor(private master: MasterService) {}

	getAll(): Observable<Player[]> {
		return this.master.get(`${this.endpoints.getAll}`);
	}

	getById(id: number): Observable<Player> {
		return this.master.get(`${this.endpoints.ById}/${id}`);
	}

	create(player: Player): Observable<Player> {
		return this.master.post(`${this.endpoints.create}`, player);
	}

	updateById(id: number, player: Player): Observable<Player> {
		return this.master.put(`${this.endpoints.ById}/${id}`, player);
	}

	deleteById(id: number): Observable<Player> {
		return this.master.delete(`${this.endpoints.ById}/${id}`);
	}
}
