import { Guid } from "guid-typescript";

export class Persona {
    nominativo: String|undefined;
    codice: Guid;

    constructor(nominativo?: String){
        this.nominativo = nominativo;
        this.codice = Guid.create();
    }
}


