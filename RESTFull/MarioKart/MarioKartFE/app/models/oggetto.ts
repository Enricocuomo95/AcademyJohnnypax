import { Guid } from "guid-typescript";
import { Persona } from "./persona";

export class Oggetto {
    nome: String;
    costo: String;
    categoria: String;
    disponibile: boolean;
    giocatore: Persona|undefined;

    constructor(nome: String, costo : String, categoria : String, disponibile : boolean = true, giocatore?: Persona){
        this.nome = nome;
        this.costo = costo;
        this.categoria = categoria;
        this.disponibile = disponibile;
        this.giocatore = giocatore;
    }
}
