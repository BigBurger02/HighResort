import {Component} from '@angular/core';
import {RoomsService} from "../../services/rooms.service";
import {DatePipe} from '@angular/common';
import {FormsModule} from "@angular/forms";
import {MatCheckboxModule} from '@angular/material/checkbox';
import {ThemePalette} from "@angular/material/core";

// EXAMPLE:
export interface Task {
  name: string;
  completed: boolean;
  subtasks?: Task[];
}

// EXAMPLE

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
  task: Task
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

  constructor(public roomsService: RoomsService, private datePipe: DatePipe) {
  }

  ngOnInit(): void {
    // console.log(this.checkInFilter, this.checkOutFilter, this.capacityFilter, this.namesFilter)

    this.roomsService.roomNames = []
    this.roomsService.structuredRooms = []
    this.roomsService.roomNames = []
    this.task = {
      name: 'All',
      completed: true,
      subtasks: [],
    };

    this.loading = true

    this.roomsService.getRoomTypes().subscribe(() => {
      for (let item of this.roomsService.roomNames) {
        this.task.subtasks?.push({name: item, completed: true})
      }
    })

    this.namesFilter = []
    if (this.task.subtasks != undefined) {
      this.task.subtasks.forEach(t => {
        if (t.completed) {
          this.namesFilter.push(t.name)
        }
      })
    }


    // this.rooms$ = this.roomsService.getAll().pipe(
    //   tap(() => this.loading = false)
    // )
    //Or that way:
    this.roomsService.getAll(this.checkInFilter, this.checkOutFilter, this.capacityFilter, this.namesFilter).subscribe(() => {
      this.loading = false
    })

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
