import { Injectable } from '@angular/core';
import { Persona } from '../models/persona';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  elencoPersone:Persona[] = JSON.parse(localStorage.getItem('elencoPersone')!) ? JSON.parse(localStorage.getItem('elencoPersone')!) : new Array();

  constructor() { 
  }

  inserisciPersonsa (nominativo?: String) : boolean {
    if(!nominativo)
      return(false);

    let i: number = 0;
    while((i<this.elencoPersone.length) && (this.elencoPersone[i].nominativo != nominativo)) i++;
    if(i < this.elencoPersone.length){
      //la persona è già stata inserita
      localStorage.setItem("personaLoggata", JSON.stringify(this.elencoPersone[i]));
      return(true);
    } 

    //altrimenti lo inserisco
    let loggato: Persona = new Persona(nominativo);
    localStorage.setItem("personaLoggata", JSON.stringify(loggato));
    this.elencoPersone.push(loggato); 
    localStorage.setItem("elencoPersone", JSON.stringify(this.elencoPersone));
    return true;
  }

  getPersonaLoggata(): Persona{
    let risposta: Persona = JSON.parse(localStorage.getItem('personaLoggata')!);
    return risposta;
  }
 
}
