import { Component } from '@angular/core';
import {RoomsService} from "../../services/rooms.service";

@Component({
  selector: 'app-room-page',
  templateUrl: './room-page.component.html'
})
export class RoomPageComponent {
  loading = false
  roomToShow: number = -1; // -1: show all

  constructor(public roomsService: RoomsService) {
  }

  ngOnInit(): void {
    this.loading = true
    // this.rooms$ = this.roomsService.getAll().pipe(
    //   tap(() => this.loading = false)
    // )
    //Or that way:
    this.roomsService.getAll().subscribe(() => {
      this.loading = false
    })
  }
}
