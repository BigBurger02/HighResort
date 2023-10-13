import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse } from "@angular/common/http";
import {catchError, delay, find, Observable, retry, tap, throwError} from "rxjs";
import {IRoom} from "../models/IRoom";
import {ErrorService} from "./error.service";
import { StructuredRoom } from "../models/StructuredRoom";
import { Room } from "../models/Room";

@Injectable({
  providedIn: 'root'
})
export class RoomsService {
  constructor(
    private http: HttpClient,
    private errorService: ErrorService
  ) { }

  structuredRooms: StructuredRoom[] = []

  getAll(): Observable<IRoom[]>{
    return this.http.get<Room[]>('https://localhost:7112/api/Room')
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
    for (let item of rooms) {
      let index = this.structuredRooms.findIndex(entry => entry.name === item.name)
      if (index != -1)
      {
        this.structuredRooms[index].room.push(item)
      }
      else {
        let newRoom: Room[] = []
        index = this.structuredRooms.push(new StructuredRoom(item.name, newRoom)) - 1

        this.structuredRooms[index].room.push(item)
      }

      // Find min and max of Capacity and Price
      // minCapacity
      if (this.structuredRooms[index].minCapacity == undefined)
      {
        this.structuredRooms[index].minCapacity = item.capacity
      }
      else if (this.structuredRooms[index].minCapacity > item.capacity)
      {
        this.structuredRooms[index].minCapacity = item.capacity
      }
      // maxCapacity
      if (this.structuredRooms[index].maxCapacity == undefined)
      {
        this.structuredRooms[index].maxCapacity = item.capacity
      }
      else if (this.structuredRooms[index].maxCapacity < item.capacity)
      {
        this.structuredRooms[index].maxCapacity = item.capacity
      }
      // minPrice
      if (this.structuredRooms[index].minPrice == undefined)
      {
        this.structuredRooms[index].minPrice = item.price
      }
      else if (this.structuredRooms[index].minPrice > item.price)
      {
        this.structuredRooms[index].minPrice = item.price
      }
      // maxPrice
      if (this.structuredRooms[index].maxPrice == undefined)
      {
        this.structuredRooms[index].maxPrice = item.price
      }
      else if (this.structuredRooms[index].maxPrice < item.price)
      {
        this.structuredRooms[index].maxPrice = item.price
      }
    }
    console.log(this.structuredRooms)
  }

  private errorHandler(error: HttpErrorResponse){
    this.errorService.handle(error.message)
    return throwError(() => error.message)
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
