import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Player } from "src/app/core/models/classes/Player";

@Component({
	selector: "player-card",
	templateUrl: "./player-card.component.html",
	styleUrls: ["./player-card.component.scss"],
})
export class PlayerCardComponent {
	@Input() player: Player;
	@Input() canEdit: boolean;
	@Output() delete = new EventEmitter<any>();
	deletePlayer(): void {
		this.delete.emit(this.player.id);
	}
}
