import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DateFilterFn } from '@angular/material/datepicker';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { RatingCreationVM } from 'src/app/shared/models/rating.model';
import { RatingService } from 'src/app/shared/rating.service';
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

  userId = this._securityService.getFieldFromJWT('id');
  today = new Date();

  showSubmitRating = false;

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
    private _reservation: ReservationService,
    private _rating: RatingService,
    private _toastr: ToastrService
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

    this._reservation.setReservationInfo(this.reservation.value);
    this._router.navigate(['/stays/reserve/' + this.stay.id]);
  }

  rate() {
    this.showSubmitRating = true;
  }

  submitRating(rating: RatingCreationVM) {
    this._rating.rate(rating).subscribe(() => {
      this._toastr.success('Successfully rated!');
      this.loadData();
      this.showSubmitRating = false;
    });
  }

  notRated() {
    if (
      this.stay.ratings.some(
        (x) => x.user == this._securityService.getFieldFromJWT('userName')
      )
    )
      return false;
    else return true;
  }

  notOwn() {
    if (this.stay.user.id == this._securityService.getFieldFromJWT('id')) {
      return false;
    } else {
      return true;
    }
  }
}
