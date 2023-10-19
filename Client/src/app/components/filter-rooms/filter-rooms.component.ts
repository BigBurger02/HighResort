import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CheckBox} from "../../models/CheckBox";
import {DatePipe} from "@angular/common";
import {RoomsService} from "../../services/rooms.service";
import {FilterRooms} from "../../models/FilterRooms";

@Component({
  selector: 'app-filter-rooms',
  templateUrl: './filter-rooms.component.html',
  providers: [DatePipe]
})
export class FilterRoomsComponent {
  @Output() submitEvent = new EventEmitter<FilterRooms>();

  filter: FilterRooms = new FilterRooms()

  task: CheckBox = {
    name: 'All',
    completed: false,
    subtasks: [],
  };
  allComplete: boolean = false;

  get formattedCheckIn() {
    return this.datePipe.transform(this.filter.checkInFilter, 'yyyy-MM-dd');
  }

  set formattedCheckIn(value) {
    let str = value?.split('-')
    if (str != undefined) {
      this.filter.checkInFilter = new Date(Number.parseInt(str[0]), (Number.parseInt(str[1]) - 1), Number.parseInt(str[2]))
    }
  }

  get formattedCheckOut() {
    return this.datePipe.transform(this.filter.checkOutFilter, 'yyyy-MM-dd');
  }

  set formattedCheckOut(value) {
    let str = value?.split('-')
    if (str != undefined) {
      this.filter.checkOutFilter = new Date(Number.parseInt(str[0]), (Number.parseInt(str[1]) - 1), Number.parseInt(str[2]))
    }
  }

  constructor(
    public roomsService: RoomsService,
    public datePipe: DatePipe
  ) {
    roomsService.getRoomTypes().subscribe(() => {
      for (let item of roomsService.roomNames) {
        if (this.task.subtasks?.findIndex(entry => entry.name == item) == -1)
        {
          this.task.subtasks?.push({name: item, completed: false})
        }
      }
    })
  }

  emitData() {
    this.filter.namesFilter = []
    if (this.task.subtasks != undefined && this.task.subtasks.length != 0) {
      this.task.subtasks.forEach(t => {
        if (t.completed) {
          this.filter.namesFilter.push(t.name)
        }
      })
    }
    this.submitEvent.emit(this.filter);
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
