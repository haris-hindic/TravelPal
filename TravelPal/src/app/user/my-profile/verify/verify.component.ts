import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { userProfileVM, userEditVM } from 'src/app/shared/models/user.model';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.css'],
})
export class VerifyComponent implements OnInit {
  @Input() user!: userProfileVM;

  code!: string;
  phoneCodeSent!: boolean;

  @Output() result = new EventEmitter<userEditVM>();
  @Output() closeModal = new EventEmitter<void>();
  constructor(
    private _user: UserService,
    private _toastr: ToastrService,
    private _security: SecurityService
  ) {}

  ngOnInit(): void {}

  close() {
    this.closeModal.emit();
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
      //this.LoadData();
      this.user.phoneNumberVerified = true;
      console.log(res);
      this._toastr.success('Success!');
    });
  }
}
