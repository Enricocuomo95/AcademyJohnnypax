import { Guid } from "guid-typescript";
import { Persona } from "./persona";

export class Oggetto {
    codice: Guid;
    nome: String;
    descrizione: String;
    possessore: Persona;

    constructor(nome: String, descrizione : String, possessore: Persona){
        this.codice = Guid.create();
        this.nome = nome;
        this.descrizione = descrizione;
        this.possessore = possessore;
    }
}
