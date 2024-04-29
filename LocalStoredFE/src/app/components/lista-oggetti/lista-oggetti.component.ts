import { Component } from '@angular/core';
import { Oggetto } from '../../models/oggetto';
import { OggettoService } from '../../services/oggetto.service';
import { Router } from '@angular/router';
import { PersonaService } from '../../services/persona.service';
import Swal from 'sweetalert2';
import { Persona } from '../../models/persona';
import { Guid } from 'guid-typescript';
import { Proposta } from '../../models/proposta';
import { PropostaService } from '../../services/proposta.service';

@Component({
  selector: 'app-lista-oggetti',
  templateUrl: './lista-oggetti.component.html',
  styleUrl: './lista-oggetti.component.css'
})
export class ListaOggettiComponent {

  lista: Oggetto[] = [];
  Nome: String|undefined;
  Index: number = 0;
  Descrizione: String|undefined;
  flagInsert: boolean = true;
  flagProposta: boolean = false;
  ProposteForObj: Proposta[] = [];
  toStringObj: String = "";
  dataOfferta: String | undefined;
  nominativoOfferente: String | undefined;

  constructor(private serviceOggetto: OggettoService, private servicePersona: PersonaService,
    private router: Router, private propostaService: PropostaService){}

  ngOnInit(){
    this.lista = this.serviceOggetto.getOggetti();
  }

  salvaOggetto(){
    if((this.Nome != undefined && this.Nome != "") && (this.Descrizione!= undefined) &&
      (!this.serviceOggetto.inserisciOggetto(this.Nome,this.Descrizione,this.servicePersona.getPersonaLoggata())))
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Si prega di completare il campo in modo corretto!",
      });
    else {
      this.lista = this.serviceOggetto.getOggetti();
      Swal.fire({
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500
      });
    }  
  }


  isVisibility(item: any): boolean{
    let possessore: Persona = item;
    let loggato: Persona = this.servicePersona.getPersonaLoggata();

    if(possessore.nominativo === loggato.nominativo)
      return true;
    return false;
  }

  ModificaOggetto(i: any){
    let index: number = i;
    this.Index = index;
    this.Nome = this.lista[index].nome;
    this.Descrizione = this.lista[index].descrizione;
    this.flagInsert = false;
  }

  EliminaOggetto(i:any){
    let index: number = i;
    this.serviceOggetto.eliminaOggetto(index);
  }

  InitProposta(i:any){
    this.Index = i; 
    this.flagProposta = true;
  }

  CreateProposta(i:any){
    let index: number = i;
    let listaOggettiBaratto: Oggetto[] = [];
    listaOggettiBaratto.push(this.lista[index]);
    listaOggettiBaratto.push(this.lista[this.Index]);
    this.propostaService.inserisciProposta(listaOggettiBaratto,this.lista[this.Index].possessore,this.lista[index].possessore, false);
    Swal.fire({
      icon: "success",
      title: "La tua proposta è stata inoltrata!",
      showConfirmButton: false,
      timer: 1500
    });
    this.flagProposta = false;
  }

  isProposta(i:any): boolean{
    let index: number = i;
    let risultato: boolean = false;
    let proposte: Proposta[] = this.propostaService.restituisciPropostePerRicevente
        (this.servicePersona.getPersonaLoggata());

    proposte.forEach(element => {
      if(element.codiceTransazione == undefined){
        let indice: number = 0;
        while((element.oggetti[indice] != undefined)
          && (element.oggetti[indice].codice === this.lista[index].codice)) indice++;
        if(element.oggetti[indice] == undefined)
          risultato = false;
        else
          risultato = true;
      }
    });
    return(risultato);
  }

  ValutaProposta(i:any){
    this.Index = 0;
    console.log("sono dentro");
    this.ProposteForObj = this.propostaService.restituisciPropostaPerOggetto(this.lista[i]);
    this.inserisciValoriPerBaratto()
  }

  inserisciValoriPerBaratto(){
    this.dataOfferta = this.ProposteForObj[this.Index].data? this.ProposteForObj[this.Index].data : undefined;
    this.nominativoOfferente = this.ProposteForObj[this.Index].offerente.nominativo;

    this.ProposteForObj[this.Index].oggetti.forEach(element => {
      this.toStringObj = this.toStringObj + "\n" +
       "nome oggetto da scambiare:\n "+ element.nome + " descrizione dell'oggetto: "+ element.descrizione;
    });
  }

  Next(){
    if(this.ProposteForObj[this.Index + 1] != undefined){
      this.Index ++;
      this.inserisciValoriPerBaratto()
    }
  }

  Accetta(){
    this.propostaService.creaTransazione(this.ProposteForObj[this.Index]);
    Swal.fire({
      icon: "success",
      title: "Grazie per aver utilizzato il nostro servizio, a presto!!",
      showConfirmButton: false,
      timer: 1500
    });
  }


  modificaOggetto(){
    if((this.Nome != undefined && this.Nome != "") && (this.Descrizione!= undefined) &&
      (!this.serviceOggetto.modificaOggetto(this.Index,this.Nome,this.Descrizione)))
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Si prega di completare il campo in modo corretto!",
      });
    else {
      this.lista = this.serviceOggetto.getOggetti();
      Swal.fire({
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500
      });
    }  
  }

}
