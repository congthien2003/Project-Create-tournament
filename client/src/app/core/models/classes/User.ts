import { IUser } from "../interfaces/IUser";

export class User implements IUser {
	id: number;
	username: string;
	email: string;
	password: string;
	phones: string;
	role: number;

	constructor() {
		this.id = 0;
		this.username = "";
		this.email = "";
		this.password = "";
		this.phones = "";
		this.role = 1;
	}
}
