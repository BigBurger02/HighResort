import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import { GlobalErrorComponent } from './components/global-error/global-error.component';
import { RoomPageComponent } from './pages/room-page/room-page.component';
import { ReservationsPageComponent } from './pages/reservations-page/reservations-page.component';
import { HomeComponent } from './components/home/home.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SpecificRoomComponent } from './components/specific-room/specific-room.component';
import {FormsModule} from "@angular/forms";
import {DatePipe} from "@angular/common";
import {MatCheckboxModule} from "@angular/material/checkbox";

@NgModule({
  declarations: [
    AppComponent,
    GlobalErrorComponent,
    RoomPageComponent,
    ReservationsPageComponent,
    HomeComponent,
    NavigationComponent,
    NotFoundComponent,
    SpecificRoomComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatCheckboxModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
