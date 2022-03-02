import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent implements OnInit {
  form!: FormGroup;
  errors: string[] = [];
  showForgotPassword: boolean = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _securityService: SecurityService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      userName: ['', { validators: [Validators.required] }],
      password: ['', { validators: [Validators.required] }],
    });
  }

  signIp() {
    this.errors = [];

    this._securityService.signIn(this.form.value).subscribe(
      (res) => {
        this._securityService.saveToken(res);
        this._router.navigate(['']);
      },
      (err) => (this.errors = parseWebAPiErrors(err))
    );
  }
}
