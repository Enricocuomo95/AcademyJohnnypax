import { Injectable } from '@angular/core';
import { Oggetto } from '../models/oggetto';
import { Persona } from '../models/persona';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { Risposta } from '../models/risposta';

@Injectable({
  providedIn: 'root'
})
export class OggettoService {
  private base_url = "http://localhost:5147/personaggio/personaggi";
  private elencoOggetti:Oggetto[] = JSON.parse(localStorage.getItem('elencoPersonaggi')!) ? JSON.parse(localStorage.getItem('elencoPersonaggi')!) : new Array(); 

  constructor(private http: HttpClient) { 
  }

  inizializza(): Observable<Oggetto[]>{
    return (this.http.get<Oggetto[]>(`${this.base_url}`));
    //localStorage.setItem("elencoPersonaggi", JSON.stringify(this.elencoOggetti));
  }

  removePersonaggio(index:number){
    this.elencoOggetti[index].giocatore = undefined;
    this.elencoOggetti[index].disponibile = true;
    localStorage.setItem("elencoPersonaggi", JSON.stringify(this.elencoOggetti));
    console.log("Oggetto eliminato");
  }


  addPersonaggio (nomePersonaggio?: String, player?: Persona) : boolean
   {
    console.log("sono nellinsert delloggetto");
    console.log(player?.username);
    console.log(player?.nominativo);
    if(player != undefined && player?.username!=null && nomePersonaggio != null && nomePersonaggio != ""){
      for (const [index, item] of this.elencoOggetti.entries()) {
        if(item.nome == nomePersonaggio){
          this.elencoOggetti[index].giocatore = player;
          this.elencoOggetti[index].disponibile = false;
          break;
        }
      }

      localStorage.setItem("elencoPersonaggi", JSON.stringify(this.elencoOggetti));
      console.log("oggetto inserito");
      return true;
    }
 
    return false;
   }

   getPersonaggi(): Oggetto[]{
    return this.elencoOggetti;
   }

   getSquadra(p: Persona): Oggetto[]{
    let risultato: Oggetto[] = new Array();

    this.elencoOggetti.forEach(element => {
      if(element.giocatore?.username == p.username)
        risultato.push(element);
    });
    return risultato;
   }
}
