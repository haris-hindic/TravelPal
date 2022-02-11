import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DateFilterFn } from '@angular/material/datepicker';
import { ActivatedRoute, Router } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import { AccommodationService } from '../accommodation.service';
import { ReservationService } from '../reservations/reservation.service';
import { AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-details',
  templateUrl: './stay-details.component.html',
  styleUrls: ['./stay-details.component.css'],
})
export class StayDetailsComponent implements OnInit {
  stay!: AccommodationVM;

  today = new Date();

  reservation!: FormGroup;

  DateFilter: DateFilterFn<Date> = (date: Date | null): boolean => {
    const m = date as Date;
    for (const x of this.stay.dateReserved) {
      if (
        m >= new Date(x.start.toString()) &&
        m <= new Date(x.end.toString())
      ) {
        return false;
      }
    }
    return true;
  };

  constructor(
    private _route: ActivatedRoute,
    private _accommodationService: AccommodationService,
    private _formBuilder: FormBuilder,
    public _securityService: SecurityService,
    private _router: Router,
    private _reservation: ReservationService
  ) {}

  ngOnInit(): void {
    this.reservation = this._formBuilder.group({
      name: '',
      start: '',
      end: '',
      price: 0,
      guestId: '',
      accommodationId: 0,
    });
    this.loadData();
  }

  loadData() {
    this._route.params.subscribe((params) => {
      this.reservation.get('accommodationId')!.patchValue(params.id);
      this._accommodationService
        .getById(params.id)
        .subscribe((data: AccommodationVM) => {
          this.stay = data;
          console.log(data);
          this.reservation.get('name')!.patchValue(data.name);
        });
    });
    this.reservation
      .get('guestId')!
      .patchValue(this._securityService.getFieldFromJWT('id'));
  }

  toReservation() {
    const start = new Date(this.reservation.get('start')?.value);
    const end = new Date(this.reservation.get('end')?.value);
    const days = Math.ceil(
      (end.valueOf() - start.valueOf()) / (1000 * 3600 * 24)
    );
    const price = days * this.stay.price;
    this.reservation.get('price')!.patchValue(price);

    console.log(JSON.stringify(this.reservation.value));

    this._reservation.setReservationInfo(this.reservation.value);
    this._router.navigate(['/stays/reserve/' + this.stay.id]);
  }
}
