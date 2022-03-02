import { TOUCH_BUFFER_MS } from '@angular/cdk/a11y/input-modality/input-modality-detector';
import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { SecurityService } from '../../security.service';

@Component({
  selector: 'app-input-new-password',
  templateUrl: './input-new-password.component.html',
  styleUrls: ['./input-new-password.component.css'],
})
export class InputNewPasswordComponent implements OnInit {
  newPassword: string = '';
  confirmPassword: string = '';
  token: string = '';
  email: string = '';
  formGroup!: FormGroup;
  errors:string[] = [];

  constructor(
    private aRoute: ActivatedRoute,
    private securityService: SecurityService,
    private route: Router,
    private toastr: ToastrService,
    private builder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.token = this.aRoute.snapshot.queryParams['token'];
    this.email = this.aRoute.snapshot.queryParams['email'];

    this.formGroup = this.builder.group(
      {
        token: [this.token,{Validators:[Validators.required]}],
        email: [this.email, {Validators:[Validators.required]}],
        password: ['', {Validators:[Validators.required]}],
        confirmPassword: ['', {Validators:[Validators.required]}]
      });
  

  }

  saveChanges() {
    if(this.checkPassword())
    {
    this.securityService
      .resetForgottenPassword(this.formGroup.value)
      .subscribe((x) => {
          this.toastr.success("Your password was changed!");
          this.route.navigateByUrl("/signIn");
      },
      err=>
      {
        this.errors = parseWebAPiErrors(err);
      });
    };

  }

  checkPassword()
  {
    let password = this.formGroup.get('password')?.value;
    let confirmPassword = this.formGroup.get('confirmPassword')?.value;
    console.log(password + '' + confirmPassword);

    if(password!=confirmPassword)
     {
       return false;
     }
     return true;
  }
}
