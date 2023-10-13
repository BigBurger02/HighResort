import {Component, Input} from '@angular/core';
import {StructuredRoom} from "../../models/StructuredRoom";

@Component({
  selector: 'app-specific-room',
  templateUrl: './specific-room.component.html'
})
export class SpecificRoomComponent {
  @Input() inputStructuredRoom: StructuredRoom;
}
