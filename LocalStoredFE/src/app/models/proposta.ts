import { Guid } from "guid-typescript";
import { Oggetto } from "./oggetto";
import { Persona } from "./persona";
import { of } from "rxjs";

export class Proposta {
    data: String;
    oggetti: Oggetto[];
    offerente: Persona;
    ricevente: Persona;
    isAccettato: boolean;
    codiceTransazione: Guid|undefined;

    constructor(oggetti: Oggetto[],offerente: Persona,ricevente: Persona,isAccettato: boolean = false){
        
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();
        this.data = dd + '/' + mm + '/' + yyyy;
        this.oggetti = oggetti;
        this.offerente = offerente;
        this.ricevente = ricevente;
        this.isAccettato = isAccettato;
    }
}
