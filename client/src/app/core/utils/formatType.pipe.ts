import { Pipe, PipeTransform } from "@angular/core";
import { FormatTypeData } from "../constant/data/format.data";

@Pipe({
	name: "formatName",
})
export class FormatTypePipe implements PipeTransform {
	formatTypeData = FormatTypeData;
	transform(formatId: number) {
		return this.formatTypeData.listFormat.find((x) => x.id === formatId)
			?.name;
	}
}
