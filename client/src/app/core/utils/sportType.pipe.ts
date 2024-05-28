import { Pipe, PipeTransform } from "@angular/core";
import { SportTypeData } from "../constant/data/sport.data";

@Pipe({
	name: "sportName",
})
export class SportTypePipe implements PipeTransform {
	formatTypeData = SportTypeData;
	transform(id: number) {
		return this.formatTypeData.listSport.find((x) => x.id === id)?.nameVie;
	}
}
