import { Injectable } from '@angular/core';
import { Proposta } from '../models/proposta';
import { Persona } from '../models/persona';
import { Oggetto } from '../models/oggetto';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class PropostaService {

  risultato:Proposta[] = [];
  elencoProposte:Proposta[] = JSON.parse(localStorage.getItem('elencoProposte')!) ? JSON.parse(localStorage.getItem('elencoProposte')!) : new Array(); 

  constructor() { 
  }

  creaTransazione(p: Proposta){
    this.elencoProposte.forEach(element =>{
      var indice = 0;
      if(element.data == p.data)
        this.elencoProposte.splice(indice,1)
      indice ++;
    })
    p.codiceTransazione = Guid.create();
    p.isAccettato = true;
    localStorage.setItem("elencoProposte", JSON.stringify(this.elencoProposte));
  }

  inserisciProposta(oggetti: Oggetto[],offerente: Persona,ricevente: Persona,isAccettato: boolean): boolean{ 

      if(oggetti && offerente && ricevente){
        this.elencoProposte.push(new Proposta(oggetti,offerente,ricevente,isAccettato));
        localStorage.setItem("elencoProposte", JSON.stringify(this.elencoProposte));
        return true;
      }
      return(false);
  }

  restituisciPropostaPerOggetto(oggetto: Oggetto){
    this.risultato = [];
    this.elencoProposte.forEach(element1 => {
      element1.oggetti.forEach(element2 => {
        if(element2.nome == oggetto.nome){
          this.risultato.push(element1);
          //Attenzione qui non legge il Guid
          //let val: Guid = (Guid) element2.codice;
          //console.log(val + " cod2: "+ oggetto.codice);
        }
      });
    });
    return this.risultato;
  }

  restituisciPropostePerRicevente(p : Persona): Proposta[] {
   
    this.elencoProposte.forEach(element => {
      if(element.ricevente.nominativo == p.nominativo)
      {
        this.risultato.push(element);
      }
     
    });
    return this.risultato;
  }


}
