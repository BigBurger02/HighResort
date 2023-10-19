import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import { GlobalErrorComponent } from './components/global-error/global-error.component';
import { RoomPageComponent } from './pages/room-page/room-page.component';
import { ReservationPageComponent } from './pages/reservation-page/reservation-page.component';
import { HomeComponent } from './components/home/home.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SpecificRoomComponent } from './components/specific-room/specific-room.component';
import {FormsModule} from "@angular/forms";
import {DatePipe} from "@angular/common";
import {MatCheckboxModule} from "@angular/material/checkbox";
import { FilterRoomsComponent } from './components/filter-rooms/filter-rooms.component';

@NgModule({
  declarations: [
    AppComponent,
    GlobalErrorComponent,
    RoomPageComponent,
    ReservationPageComponent,
    HomeComponent,
    NavigationComponent,
    NotFoundComponent,
    SpecificRoomComponent,
    FilterRoomsComponent
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
