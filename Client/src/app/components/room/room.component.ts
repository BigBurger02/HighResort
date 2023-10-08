import {Component, Input} from "@angular/core";
import {IRoom} from "../../models/room";

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html'
})
export class RoomComponent {
  @Input() room: IRoom
}
