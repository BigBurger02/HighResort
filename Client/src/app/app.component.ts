import { Component } from '@angular/core';
import {IRoom} from "./models/room";
import {rooms as data} from "./data/rooms";
import {RoomsService} from "./services/rooms.service";
import {Observable, tap} from "rxjs";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

}
