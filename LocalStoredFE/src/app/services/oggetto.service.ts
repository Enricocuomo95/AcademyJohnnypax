import { Injectable } from '@angular/core';
import { Oggetto } from '../models/oggetto';
import { Persona } from '../models/persona';

@Injectable({
  providedIn: 'root'
})
export class OggettoService {
  private elencoOggetti:Oggetto[] = JSON.parse(localStorage.getItem('elencoOggetti')!) ? JSON.parse(localStorage.getItem('elencoOggetti')!) : new Array(); 

  constructor() { 
  }

  modificaOggetto (index: number, nome?: String, descrizione?: String) : boolean
  {
    if(nome != undefined && descrizione != undefined){
      this.elencoOggetti[index].descrizione = descrizione;
      this.elencoOggetti[index].nome = nome;
      
      localStorage.setItem("elencoOggetti", JSON.stringify(this.elencoOggetti));
      console.log("oggetto modificato");
      return true;
    }
    return false;
  }

  eliminaOggetto(index:number){
    this.elencoOggetti.splice(index,1);
    localStorage.setItem("elencoOggetti", JSON.stringify(this.elencoOggetti));
    console.log("Oggetto eliminato");
  }


  inserisciOggetto (nome: String, descrizione: String, possessore: Persona) : boolean
   {
    console.log("sono nellinsert delloggetto");
    console.log(possessore.codice);
    console.log(possessore.nominativo);
    if(possessore != undefined && possessore?.codice!=null){
      this.elencoOggetti.push(new Oggetto(nome,descrizione,possessore));
      localStorage.setItem("elencoOggetti", JSON.stringify(this.elencoOggetti));
      console.log("oggetto inserito");
      return true;
    }
 
    return false;
   }

   getOggetti(): Oggetto[]{
    return this.elencoOggetti;
   }

   getOggettoForPersona(p: Persona): Oggetto[]{
    let risultato: Oggetto[] = new Array();

    this.elencoOggetti.forEach(element => {
      if(element.possessore.codice == p.codice)
        risultato.push(element);
    });
    return risultato;
   }
}
