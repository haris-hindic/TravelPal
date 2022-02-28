import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { ReservationVM } from '../reservation.model';
import { ReservationService } from '../reservation.service';

@Component({
  selector: 'app-host-reservations',
  templateUrl: './host-reservations.component.html',
  styleUrls: ['./host-reservations.component.css'],
})
export class HostReservationsComponent implements OnInit {
  reservations!: ReservationVM[];

  constructor(
    private _reservation: ReservationService,
    private _toastr: ToastrService,
    public _securityService: SecurityService
  ) {}
  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._reservation
      .getByHost(this._securityService.getFieldFromJWT('id'))
      .subscribe((x) => {
        this.reservations = x;
      });
  }

  confirm(id: number) {
    this._reservation.confirm(id).subscribe(() => {
      this.loadData();
      this._toastr.info('Successfully confirmed the reservation!');
    });
  }
}
