import { Component, ViewChild } from '@angular/core';
import { PersonaService } from '../../services/persona.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
    Nominativo: String | undefined;
    constructor(private service: PersonaService, private router: Router){ }
  
    aggiungiPersona(){
      if((this.Nominativo?.trim() == "") || (!this.service.inserisciPersonsa(this.Nominativo)))
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Si prega di completare il campo in modo corretto!",
        });
      else

      this.router.navigate(['lista']);
    }

}
