import { Guid } from "guid-typescript";

export class Persona {
    nominativo: String | undefined;
    username: String | undefined;
    passward: String | undefined;

    constructor(nominativo?: String, username?: String, passward?: String){
        this.nominativo = nominativo;
        this.username = username;
        this.passward = passward;
    }
}


