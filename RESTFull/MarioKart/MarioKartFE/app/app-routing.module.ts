import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaOggettiComponent } from './components/lista-oggetti/lista-oggetti.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {path: "", redirectTo: "inserisci", pathMatch: "full"},
  {path: "lista", component: ListaOggettiComponent},
  {path: "inserisci", component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
