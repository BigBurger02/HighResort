import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RoomPageComponent} from "./pages/room-page/room-page.component";
import {ReservationsPageComponent} from "./pages/reservations-page/reservations-page.component";
import {HomeComponent} from "./components/home/home.component";
import {NotFoundComponent} from "./components/not-found/not-found.component";

const routes: Routes = [
  { path: '',   redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'rooms', component: RoomPageComponent },
  { path: 'reservations', component: ReservationsPageComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
