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
import { UserService } from '../user.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent implements OnInit {
  user!: userProfileVM;
  code!: string;

  constructor(
    private _security: SecurityService,
    private _user: UserService,
    private _http: HttpClient,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.LoadData();
  }

  LoadData() {
    const id = this._security.getFieldFromJWT('id');
    // this._http
    //   .get<userProfileVM>(
    //     `https://localhost:44325/api/Accounts/profile?id=${id}`
    //   )
    //   .subscribe((res) => {
    //     this.user = res;
    //     console.log(res);
    //   });

    this._user.getUserById(id).subscribe((res) => {
      this.user = res;
    });
  }

  resend() {
    // this._http
    //   .get<userProfileVM>(
    //     `https://localhost:44325/api/Accounts/resend-email?email=${this.user.email}`
    //   )
    //   .subscribe(() => {
    //     this._toastr.success('Please check your email.');
    //   });
    this._user.sendEmailVerification(this.user.email).subscribe(() => {
      this._toastr.success('Please check your email.');
    });
  }

  sendPhoneVerification() {
    const id = this._security.getFieldFromJWT('id');
    // this._http
    //   .get<userProfileVM>(
    //     `https://localhost:44325/api/Accounts/phone-verification?id=${id}`
    //   )
    //   .subscribe((res) => {
    //     console.log(res);
    //     this._toastr.success('Check your inbox!');
    //   });

    this._user.sendPhoneVerification(id).subscribe((res) => {
      console.log(res);
      this._toastr.success('Check your inbox!');
    });
  }

  submitCode() {
    const id = this._security.getFieldFromJWT('id');
    // this._http
    //   .get<userProfileVM>(
    //     `https://localhost:44325/api/Accounts/check-phone-verification?id=${id}&code=${this.code}`
    //   )
    //   .subscribe((res) => {
    //     this.LoadData();
    //     console.log(res);
    //     this._toastr.success('Success!');
    //   });

    this._user.checkPhoneVerification(id, this.code).subscribe((res) => {
      this.LoadData();
      console.log(res);
      this._toastr.success('Success!');
    });
  }
}
