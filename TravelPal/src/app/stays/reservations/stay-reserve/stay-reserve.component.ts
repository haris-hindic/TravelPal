import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  ReservationCreationVM,
  ReservationUserInfoVM,
} from '../reservation.model';

@Component({
  selector: 'app-stay-reserve',
  templateUrl: './stay-reserve.component.html',
  styleUrls: ['./stay-reserve.component.css'],
})
export class StayReserveComponent implements OnInit {
  userInfo!: ReservationUserInfoVM;
  reservation!: ReservationCreationVM;

  form!: FormGroup;

  constructor(private _formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      firstName: ['', { validators: [Validators.required] }],
      lastName: ['', { validators: [Validators.required] }],
      email: ['', { validators: [Validators.required] }],
      phoneNumber: ['', { validators: [Validators.required] }],
      hostId: [``],
      paymentInfo: this._formBuilder.group({
        country: ['', { validators: [Validators.required] }],
        city: ['', { validators: [Validators.required] }],
        postalCode: ['', { validators: [Validators.required] }],
        ccNumber: ['', { validators: [Validators.required] }],
        expMonth: ['', { validators: [Validators.required] }],
        ccvCode: ['', { validators: [Validators.required] }],
      }),
    });
  }

  submit() {}
}
