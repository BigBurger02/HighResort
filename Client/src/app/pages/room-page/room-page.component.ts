import {Component, ViewChild} from '@angular/core';
import {RoomsService} from "../../services/rooms.service";
import {DatePipe} from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {Room} from "../../models/Room";
import {FilterRoomsComponent} from "../../components/filter-rooms/filter-rooms.component"
import {FilterRooms} from "../../models/FilterRooms";
import { Router } from "@angular/router"
import {ErrorService} from "../../services/error.service";

@Component({
  selector: 'app-room-page',
  templateUrl: './room-page.component.html',
  providers: [DatePipe, MatCheckboxModule],
})
export class RoomPageComponent {
  loading = false
  roomToShow: number = -1; // -1: show all

  filterData: FilterRooms = new FilterRooms()

  @ViewChild(FilterRoomsComponent, { static: false }) filterRoomsComponent: FilterRoomsComponent
    = new FilterRoomsComponent(this.roomsService, this.datePipe)

  constructor(
    public roomsService: RoomsService,
    public datePipe: DatePipe,
    private router: Router,
    private errorService: ErrorService,
  ) { }

  ngOnInit(): void {
    this.roomsService.roomNames = []
    this.roomsService.structuredRooms = []
    this.roomsService.roomNames = []

    this.loading = true
    this.roomsService.getAll(this.filterData).subscribe(() => {
      this.loading = false
    })
  }

  filterDuplicates(rooms: Room[]): Room[] {
    const filteredRooms: Room[] = [];
    const seenRooms = new Set<string>();

    for (const room of rooms) {
      const roomKey = `${room.capacity}_${room.price}`;
      if (!seenRooms.has(roomKey)) {
        filteredRooms.push(room);
        seenRooms.add(roomKey);
      }
    }

    return filteredRooms;
  }

  handleFilterData(data: FilterRooms) {
    this.filterData = data;
    this.ngOnInit()
  }

  reservationRedirect(checkIn: string | null, checkOut: string | null, roomId: number) {
    if (checkIn == null || checkOut == null || roomId == 0) {
      console.log('Wrong data while redirection:', checkIn, checkOut, roomId)
      this.errorService.handle('Some problem. Wrong data')
    }
    else {
      this.router.navigate(['/reservation', checkIn, checkOut, roomId]);
    }
  }
}
