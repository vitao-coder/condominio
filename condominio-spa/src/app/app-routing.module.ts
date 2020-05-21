
import { AuthGuard } from '../helpers/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ApartamentoComponent } from './../components/apartamento/apartamento.component';
import { LoginComponent } from './../components/login/login.component';
import { MoradorComponent } from './../components/morador/morador.component';


const routes: Routes = [
  { path: '', component: ApartamentoComponent, canActivate: [AuthGuard] },
  { path: 'apartamento', component: ApartamentoComponent, canActivate: [AuthGuard] },
  { path: 'morador', component: MoradorComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent , pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
