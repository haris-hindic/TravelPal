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

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent implements OnInit {
  user!: userProfileVM;

  constructor(
    private _security: SecurityService,
    private _http: HttpClient,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const id = this._security.getFieldFromJWT('id');
    this._http
      .get<userProfileVM>(
        `https://localhost:44325/api/Accounts/profile?id=${id}`
      )
      .subscribe((res) => {
        this.user = res;
      });
  }

  resend() {
    this._http
      .get<userProfileVM>(
        `https://localhost:44325/api/Accounts/resend-email?email=${this.user.email}`
      )
      .subscribe(() => {
        this._toastr.success('Please check your email.');
      });
  }
}
