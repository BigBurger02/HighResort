import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpParams} from "@angular/common/http";
import {catchError, delay, Observable, retry, tap, throwError} from "rxjs";
import {IRoom} from "../models/room";
import {ErrorService} from "./error.service";

@Injectable({
  providedIn: 'root'
})
export class RoomsService {
  constructor(
    private http: HttpClient,
    private errorService: ErrorService
  ) { }

  rooms: IRoom[] = []

  getAll(): Observable<IRoom[]>{
    return this.http.get<IRoom[]>('https://localhost:7112/api/Room')
      .pipe(
        delay(2000),
        retry(2),
        tap(rooms => this.rooms = rooms),
        catchError(this.errorHandler.bind(this))
      )
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
