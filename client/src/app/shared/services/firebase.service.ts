import { Injectable } from "@angular/core";
import { AngularFireStorage } from "@angular/fire/compat/storage";
import { Observable } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class FirebaseService {
	constructor(private fireStorage: AngularFireStorage) {}

	async saveFile(path: string, data: File): Promise<string> {
		if (path === "" && data === null) return "";
		else {
			path += "/" + data.name;
			const uploadTask = await this.fireStorage.upload(path, data);
			const url = uploadTask.ref.getDownloadURL();
			return url;
		}
	}

	deleteFile(imageRef: any): void {
		imageRef.child("name").delete();
	}

	getImageRefFromUrl(imageUrl: string) {
		if (typeof imageUrl === "string") {
			const imageRef = this.fireStorage.refFromURL(imageUrl);

			return imageRef;
		} else {
			return null;
		}
	}
}
