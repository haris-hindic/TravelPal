import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent implements OnInit {
  form!: FormGroup;
  errors: string[] = [];

  constructor(
    private _formBuilder: FormBuilder,
    private _securityService: SecurityService,
    private _router: Router,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      userName: ['', { validators: [Validators.required] }],
      email: ['', { validators: [Validators.required, Validators.email] }],
      password: ['', { validators: [Validators.required] }],
      firstName: ['', { validators: [Validators.required] }],
      lastName: ['', { validators: [Validators.required] }],
      phoneNumber: ['', { validators: [Validators.required] }],
    });
  }

  signUp() {
    console.log(this.form.value);
    this.errors = [];

    this._securityService.signUp(this.form.value).subscribe(
      (res) => {
        this._toastr.success('Please verify your email address!');
        this._toastr.info(
          'In order to host or reserve you need to verify your profile.'
        );
        this._securityService.saveToken(res);
        this._router.navigate(['']);
      },
      (err) => (this.errors = parseWebAPiErrors(err))
    );
  }
}
