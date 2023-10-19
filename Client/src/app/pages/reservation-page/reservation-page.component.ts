import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation-page',
  templateUrl: './reservation-page.component.html'
})
export class ReservationPageComponent {
  checkIn: string
  checkOut: string
  roomId: number

  constructor(
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe((params) => {
      this.checkIn = params.param1;
      this.checkOut = params.param2;
      this.roomId = +params.param3;
      console.log(this.checkIn, this.checkOut, this.roomId)
    });
  }
}
