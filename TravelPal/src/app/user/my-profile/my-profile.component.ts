import {
  HttpClient,
  HttpHeaderResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { userProfileVM } from 'src/app/shared/models/user.model';
import { AccommodationService } from 'src/app/stays/accommodation.service';
import { AccommodationVM } from 'src/app/stays/stays.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent implements OnInit {
  user!: userProfileVM;
  userStays!: AccommodationVM[];
  code!: string;
  phoneCodeSent!: boolean;

  constructor(
    private _security: SecurityService,
    private _user: UserService,
    private _accommodation: AccommodationService,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.LoadData();
  }

  LoadData() {
    const id = this._security.getFieldFromJWT('id');

    this._user.getUserById(id).subscribe((res) => {
      this.user = res;
    });
    this._accommodation.getByUser(id).subscribe((res) => {
      this.userStays = res;
    });
  }

  resend() {
    this._user.sendEmailVerification(this.user.email).subscribe(() => {
      this._toastr.success('Please check your email.');
    });
  }

  sendPhoneVerification() {
    const id = this._security.getFieldFromJWT('id');

     this._user.sendPhoneVerification(id).subscribe((res) => {
       console.log(res);
     });
    this._toastr.success('Check your inbox!');
    this.phoneCodeSent = true;
  }

  submitCode() {
    const id = this._security.getFieldFromJWT('id');

    this._user.checkPhoneVerification(id, this.code).subscribe((res) => {
      this.LoadData();
      console.log(res);
      this._toastr.success('Success!');
    });
  }
}
