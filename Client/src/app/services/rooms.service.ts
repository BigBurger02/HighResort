import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpParams} from "@angular/common/http";
import {catchError, delay, find, findIndex, Observable, retry, tap, throwError} from "rxjs";
import {ErrorService} from "./error.service";
import { StructuredRoom } from "../models/StructuredRoom";
import { Room } from "../models/Room";
import { DatePipe } from '@angular/common';
import {RoomType} from "../models/RoomType";

@Injectable({
  providedIn: 'root'
})
export class RoomsService {
  constructor(
    private http: HttpClient,
    private errorService: ErrorService,
    private datePipe: DatePipe
  ) { }

  structuredRooms: StructuredRoom[] = []
  roomNames: string[] = []

  getAll(checkIn: Date, checkOut: Date, capacity: number, names: string[]): Observable<Room[]> {
    let queryString: string = ''
    if (checkIn != undefined)
    {
      let formattedDate = `checkIn=${this.datePipe.transform(checkIn, 'MM-dd-yyyy')}`;
      queryString = queryString.concat(formattedDate)
    }
    if (checkOut != undefined)
    {
      let formattedDate = `checkOut=${this.datePipe.transform(checkOut, 'MM-dd-yyyy')}`;
      queryString = queryString.concat('&', formattedDate)
    }
    if (capacity != undefined && capacity != 0)
    {
      let formattedCapacity = `capacity=${String(capacity.toString())}`;
      queryString = queryString.concat('&', formattedCapacity)
    }
    if (names != undefined && names.length != 0)
    {
      let formattedNames = ''
      names.forEach(n => {
        formattedNames = formattedNames.concat('names=', n, '&')
      })
      queryString = queryString.concat('&', formattedNames)
    }

    let params: HttpParams = new HttpParams({fromString: queryString})

    return this.http.get<Room[]>('https://localhost:7112/api/BookARoom', {
        params: params
      })
      .pipe(
        delay(200),
        retry(2),
        tap(
          rooms => this.transformData(rooms)
        ),
        catchError(this.errorHandler.bind(this))
      )
  }

  private transformData(rooms: Room[]) {
    console.log(rooms)

    for (let item of rooms) {
      let index = this.structuredRooms.findIndex(entry => entry.name === item.name)
      if (index == -1) {
        let newRoom: Room[] = []
        index = this.structuredRooms.push(new StructuredRoom(item.name, newRoom)) - 1
      }
      // Distinct:
      // if (
      //   this.structuredRooms[index].room.findIndex(entry => entry.capacity === item.capacity) == -1
      // ) {
      //   this.structuredRooms[index].room.push(item)
      // }
      this.structuredRooms[index].room.push(item)

      // Find min and max of Capacity and Price
      this.minmax(index, item.capacity, item.price)
    }

    console.log(this.structuredRooms)
  }

  minmax(index: number, capacity: number, price: number) {
    // minCapacity
    if (this.structuredRooms[index].minCapacity == undefined) {
      this.structuredRooms[index].minCapacity = capacity
    } else if (this.structuredRooms[index].minCapacity > capacity) {
      this.structuredRooms[index].minCapacity = capacity
    }
    // maxCapacity
    if (this.structuredRooms[index].maxCapacity == undefined) {
      this.structuredRooms[index].maxCapacity = capacity
    } else if (this.structuredRooms[index].maxCapacity < capacity) {
      this.structuredRooms[index].maxCapacity = capacity
    }
    // minPrice
    if (this.structuredRooms[index].minPrice == undefined) {
      this.structuredRooms[index].minPrice = price
    } else if (this.structuredRooms[index].minPrice > price) {
      this.structuredRooms[index].minPrice = price
    }
    // maxPrice
    if (this.structuredRooms[index].maxPrice == undefined) {
      this.structuredRooms[index].maxPrice = price
    } else if (this.structuredRooms[index].maxPrice < price) {
      this.structuredRooms[index].maxPrice = price
    }
  }

  private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message)
    return throwError(() => error.message)
  }

  getRoomTypes() {
    return this.http.get<RoomType[]>('https://localhost:7112/api/RoomType')
      .pipe(
        tap(
          rooms => {
            rooms.forEach(r => {
              this.roomNames.push(r.name)
            })
          }
        ),
        catchError(this.errorHandler.bind(this))
      )
  }

  // queryStringExample1(): Observable<IRoom[]>{
  //   return this.http.get<IRoom[]>('https://localhost:7112/api/Room', {
  //     params: new HttpParams().append('limit', 5)
  //   })
  // }
  // queryStringExample2(): Observable<IRoom[]>{
  //   return this.http.get<IRoom[]>('https://localhost:7112/api/Room', {
  //     params: new HttpParams({
  //       fromString: 'limit=5'
  //     })
  //   })
  // }
  // queryStringExample3(): Observable<IRoom[]>{
  //   return this.http.get<IRoom[]>('https://localhost:7112/api/Room', {
  //     params: new HttpParams({
  //       fromObject: {limit: 5}
  //     })
  //   })
  // }
}
