import { AppRoutingModule } from './app-routing.module';

import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { MaterialDesignModule } from '../app/material-design.module';

import { fakeBackendProvider } from '../helpers/fake-backend';
import { JwtInterceptor } from './../helpers/jwt.interceptor';
import { ErrorInterceptor } from './../helpers/error.interceptor';

import { AppComponent } from './app.component';
import { LoginComponent } from './../components/login/login.component';
import { NavCondominioComponent } from '../components/nav-condominio/nav-condominio.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavCondominioComponent
    ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialDesignModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  /*fakeBackendProvider*/],
  bootstrap: [AppComponent]
})
export class AppModule { }
