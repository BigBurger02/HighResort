import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RoomPageComponent} from "./pages/room-page/room-page.component";
import {ReservationsPageComponent} from "./pages/reservations-page/reservations-page.component";
import {HomeComponent} from "./components/home/home.component";

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'rooms', component: RoomPageComponent },
  { path: 'reservations', component: ReservationsPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
