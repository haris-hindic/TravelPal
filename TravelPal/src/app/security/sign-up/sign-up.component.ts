import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
    private _router: Router
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
        this._securityService.saveToken(res);
        this._router.navigate(['']);
      },
      (err) => (this.errors = parseWebAPiErrors(err))
    );
  }
}
