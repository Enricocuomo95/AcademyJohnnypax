import { Component } from '@angular/core';
import { Oggetto } from '../../models/oggetto';
import { OggettoService } from '../../services/oggetto.service';
import { Router } from '@angular/router';
import { PersonaService } from '../../services/persona.service';
import Swal from 'sweetalert2';
import { Persona } from '../../models/persona';

@Component({
  selector: 'app-lista-oggetti',
  templateUrl: './lista-oggetti.component.html',
  styleUrl: './lista-oggetti.component.css'
})
export class ListaOggettiComponent {

  lista: Oggetto[] = [];
  crediti: number = 10;
  constructor(private servicePersonaggio: OggettoService, private servicePlayer: PersonaService,private router: Router){}



  ngOnInit(){
    this.servicePersonaggio.inizializza().subscribe(risultato =>{
      this.lista = risultato;
      console.log(this.lista);
    })
    //this.lista = this.servicePersonaggio.getPersonaggi();
  }

  addPersonaggio(i: any){
    let val: any = this.lista[i].costo;
    let costo: number = val;
    console.log(costo);
    if((this.lista[i].nome!= undefined && this.servicePlayer.getPersonaLoggata() != undefined) && 
      (this.servicePersonaggio.getSquadra(this.servicePlayer.getPersonaLoggata()).length < 3) &&
      ((this.crediti - costo) > -1)) {
      this.servicePersonaggio.addPersonaggio(this.lista[i].nome,this.servicePlayer.getPersonaLoggata());
      this.lista[i].disponibile = false;
      this.crediti = this.crediti - costo;
      Swal.fire({
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500
      });
    } else {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Si prega di completare il campo in modo corretto!",
      });
      
    }  
  }


  rimuoviPersonaggio(i: any){
    if(this.lista[i].nome != undefined) {
      this.servicePersonaggio.removePersonaggio(i);
      this.lista[i].disponibile = true;
      Swal.fire({
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500
      });
      
    } else {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Si prega di completare il campo in modo corretto!",
      });
    }  
  }
}
