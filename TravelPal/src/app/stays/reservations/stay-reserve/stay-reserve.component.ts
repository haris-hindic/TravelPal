import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import {
  ReservationTempInfo,
  ReservationUserInfoVM,
} from '../reservation.model';
import { ReservationService } from '../reservation.service';

@Component({
  selector: 'app-stay-reserve',
  templateUrl: './stay-reserve.component.html',
  styleUrls: ['./stay-reserve.component.css'],
})
export class StayReserveComponent implements OnInit {
  userInfo!: ReservationUserInfoVM;
  reservation!: ReservationTempInfo;

  form!: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private _reservation: ReservationService,
    private _security: SecurityService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this._reservation
      .getUserInfo(this._security.getFieldFromJWT('id'))
      .subscribe((userInfo) => (this.userInfo = userInfo));

    this.form = this._formBuilder.group({
      start: ['', { validators: [Validators.required] }],
      end: ['', { validators: [Validators.required] }],
      price: [0, { validators: [Validators.required] }],
      accommodationId: ['', { validators: [Validators.required] }],
      guestId: [``],
      paymentInfo: this._formBuilder.group({
        country: ['', { validators: [Validators.required] }],
        city: ['', { validators: [Validators.required] }],
        postalCode: ['', { validators: [Validators.required] }],
        ccNumber: ['', { validators: [Validators.required] }],
        expDate: ['', { validators: [Validators.required] }],
        ccvCode: ['', { validators: [Validators.required] }],
      }),
    });
    this.loadData();
  }

  loadData() {
    this.reservation = this._reservation.getReservationInfo();

    this.form.get('start')?.patchValue(this.reservation.start);
    this.form.get('end')?.patchValue(this.reservation.end);
    this.form.get('price')?.patchValue(this.reservation.price);
    this.form
      .get('accommodationId')
      ?.patchValue(this.reservation.accommodationId);
    this.form.get('guestId')?.patchValue(this.reservation.guestId);
  }

  submit() {
    this._reservation.create(this.form.value).subscribe(() => {
      this._reservation.removeReservationInfo();
      this._router.navigate(['/user-reservations']);
    });
  }
}
