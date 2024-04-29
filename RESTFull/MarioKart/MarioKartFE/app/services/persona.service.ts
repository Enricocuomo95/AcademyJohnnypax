import { Injectable } from '@angular/core';
import { Persona } from '../models/persona';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  private base_url = "http://localhost:5147/player/inserisciPlayer";
  elencoPersone:Persona[] = JSON.parse(localStorage.getItem('elencoPlayer')!) ? JSON.parse(localStorage.getItem('elencoPlayer')!) : new Array();

  constructor(private http: HttpClient) { 
  }

  inserisciPeyerNelDB(objPlayer : Persona){
    let headerCustom = new HttpHeaders();
    headerCustom.set('Content-Type','Application/Json');
    console.log("sono dentro");
    console.log(objPlayer.username);
    this.http.post(this.base_url, objPlayer, {headers : headerCustom});
  }

  inserisciPersonsa (nominativo?: String, username?: String, password?: String) : boolean {
    if((!nominativo)||(!username)||(!password))
      return(false);

    let i: number = 0;
    while((i<this.elencoPersone.length) && (this.elencoPersone[i].username != username)) i++;
    if(i < this.elencoPersone.length){
      //la persona è già stata inserita
      localStorage.setItem("playerLoggato", JSON.stringify(this.elencoPersone[i]));
      return(true);
    } 

    //altrimenti lo inserisco
    let loggato: Persona = new Persona(nominativo,username,password);
    localStorage.setItem("playerLoggato", JSON.stringify(loggato));
    this.elencoPersone.push(loggato); 
    localStorage.setItem("elencoPlayer", JSON.stringify(this.elencoPersone));
    return true;
  }

  getPersonaLoggata(): Persona{
    let risposta: Persona = JSON.parse(localStorage.getItem('playerLoggato')!);
    return risposta;
  }
 
}
