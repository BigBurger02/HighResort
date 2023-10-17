import {Component} from '@angular/core';
import {RoomsService} from "../../services/rooms.service";
import {DatePipe} from '@angular/common';
import {FormsModule} from "@angular/forms";
import {MatCheckboxModule} from '@angular/material/checkbox';
import {ThemePalette} from "@angular/material/core";
import {CheckBox} from "../../models/CheckBox";
import {Room} from "../../models/Room";

@Component({
  selector: 'app-room-page',
  templateUrl: './room-page.component.html',
  providers: [DatePipe, MatCheckboxModule],
})
export class RoomPageComponent {
  loading = false
  roomToShow: number = -1; // -1: show all

  checkInFilter: Date = new Date(2023, 8, 4)
  checkOutFilter: Date = new Date(2023, 8, 6)
  capacityFilter: number
  namesFilter: string[]

  // Room type MatCheckboxModule:
  task: CheckBox = {
    name: 'All',
    completed: false,
    subtasks: [],
  };
  allComplete: boolean = false;

  get formattedCheckIn() {
    return this.datePipe.transform(this.checkInFilter, 'yyyy-MM-dd');
  }

  set formattedCheckIn(value) {
    let str = value?.split('-')
    if (str != undefined) {
      this.checkInFilter = new Date(Number.parseInt(str[0]), (Number.parseInt(str[1]) - 1), Number.parseInt(str[2]))
    }
  }

  get formattedCheckOut() {
    return this.datePipe.transform(this.checkOutFilter, 'yyyy-MM-dd');
  }

  set formattedCheckOut(value) {
    let str = value?.split('-')
    if (str != undefined) {
      this.checkOutFilter = new Date(Number.parseInt(str[0]), (Number.parseInt(str[1]) - 1), Number.parseInt(str[2]))
    }
  }

  constructor(public roomsService: RoomsService, public datePipe: DatePipe) {
    this.roomsService.getRoomTypes().subscribe(() => {
      for (let item of this.roomsService.roomNames) {
        this.task.subtasks?.push({name: item, completed: false})
      }
    })
  }

  ngOnInit(): void {
    // console.log(this.checkInFilter, this.checkOutFilter, this.capacityFilter, this.namesFilter)

    this.roomsService.roomNames = []
    this.roomsService.structuredRooms = []
    this.roomsService.roomNames = []


    // console.log(this.task.subtasks?.length)
    this.namesFilter = []
    if (this.task.subtasks != undefined && this.task.subtasks.length != 0) {
      this.task.subtasks.forEach(t => {
        if (t.completed) {
          this.namesFilter.push(t.name)
        }
      })
    }

    this.loading = true
    // this.rooms$ = this.roomsService.getAll().pipe(
    //   tap(() => this.loading = false)
    // )
    //Or that way:
    this.roomsService.getAll(this.checkInFilter, this.checkOutFilter, this.capacityFilter, this.namesFilter).subscribe(() => {
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

  // Room type MatCheckboxModule:
  updateAllComplete() {
    this.allComplete = this.task.subtasks != null && this.task.subtasks.every(t => t.completed);
  }

  someComplete(): boolean {
    if (this.task.subtasks == null) {
      return false;
    }
    return this.task.subtasks.filter(t => t.completed).length > 0 && !this.allComplete;
  }

  setAll(completed: boolean) {
    this.allComplete = completed;
    if (this.task.subtasks == null) {
      return;
    }
    this.task.subtasks.forEach(t => (t.completed = completed));
  }
}
