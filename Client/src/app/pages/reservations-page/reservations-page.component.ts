import { Component } from '@angular/core';
import {RoomType} from "../../models/RoomType";
import {catchError, delay, retry, tap, throwError} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ErrorService} from "../../services/error.service";

@Component({
  selector: 'app-reservations-page',
  templateUrl: './reservations-page.component.html'
})
export class ReservationsPageComponent {
}
