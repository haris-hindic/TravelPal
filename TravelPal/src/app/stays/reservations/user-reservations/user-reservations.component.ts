import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { AccommodationService } from '../../accommodation.service';
import { ReservationVM } from '../reservation.model';
import { ReservationService } from '../reservation.service';

@Component({
  selector: 'app-user-reservations',
  templateUrl: './user-reservations.component.html',
  styleUrls: ['./user-reservations.component.css'],
})
export class UserReservationsComponent implements OnInit {
  reservations!: ReservationVM[];

  constructor(
    private _reservation: ReservationService,
    private _toastr: ToastrService,
    public _securityService: SecurityService
  ) {}
  ngOnInit(): void {
    this._reservation
      .getByUser(this._securityService.getFieldFromJWT('id'))
      .subscribe((x) => {
        this.reservations = x;
      });
  }

  loadData() {
    this._reservation
      .getByUser(this._securityService.getFieldFromJWT('id'))
      .subscribe((x) => {
        this.reservations = x;
      });
  }

  cancel(id: number) {
    this._reservation.cancel(id).subscribe(() => {
      this.loadData();
      this._toastr.info('Successfully cancelled your reservation!');
    });
  }
}
