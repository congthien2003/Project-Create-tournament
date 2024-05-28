import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class IdloaderService {
	constructor() {}

	private currentId = new BehaviorSubject<number | null>(null);

	currentId$ = this.currentId.asObservable();

	setCurrentId(id: number) {
		this.currentId.next(id);
	}
}
