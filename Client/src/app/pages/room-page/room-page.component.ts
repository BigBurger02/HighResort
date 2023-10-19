import {Component, ViewChild} from '@angular/core';
import {RoomsService} from "../../services/rooms.service";
import {DatePipe} from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {Room} from "../../models/Room";
import {FilterRoomsComponent} from "../../components/filter-rooms/filter-rooms.component"
import {FilterRooms} from "../../models/FilterRooms";

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
    public datePipe: DatePipe
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

  handleChildData(data: FilterRooms) {
    this.filterData = data;
    this.ngOnInit()
  }
}
