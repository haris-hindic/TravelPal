import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { userProfileVM } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/user/user.service';
import { EventVM } from '../../events.model';
import { EventsService } from '../../events.service';
import { EventsSignUpService } from '../events-sign-up.service';

@Component({
  selector: 'app-events-sign-up-creation',
  templateUrl: './events-sign-up-creation.component.html',
  styleUrls: ['./events-sign-up-creation.component.css'],
})
export class EventsSignUpCreationComponent implements OnInit {
  formGroup!: FormGroup;
  formGroupUserInfo!: FormGroup;
  user!: userProfileVM;
  event!: EventVM;
  id = this.securityService.getFieldFromJWT('id');

  constructor(
    private builder: FormBuilder,
    private securityService: SecurityService,
    private userService: UserService,
    private aRoute: ActivatedRoute,
    private eventService: EventsService,
    private eventSignUpService: EventsSignUpService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.aRoute.params.subscribe((x) => {
      let id = 0;
      id = +x['id'];
      this.eventService.getSpecific(id).subscribe((x) => {
        this.event = x;
      });
    });

    this.formGroup = this.builder.group({
      price: [0],
      eventParticipantId: [this.id],
      eventId: [0, { validators: [Validators.required] }],
      paymentInfo: this.builder.group({
        country: ['', { validators: [Validators.required] }],
        city: ['', { validators: [Validators.required] }],
        postalCode: ['', { validators: [Validators.required] }],
        ccNumber: ['', { validators: [Validators.required] }],
        expDate: ['', { validators: [Validators.required] }],
        ccvCode: ['', { validators: [Validators.required] }],
      }),
    });

    this.formGroupUserInfo = this.builder.group({
      firstName: [''],
      lastName: [''],
      email: [''],
    });

    this.userService.getUserById(this.id).subscribe((x) => {
      this.user = x;

      this.patchValue();
    });
  }

  patchValue() {
    console.log(this.user);
    this.formGroupUserInfo.get('firstName')?.patchValue(this.user.firstName);
    this.formGroupUserInfo.get('lastName')?.patchValue(this.user.lastName);
    this.formGroupUserInfo.get('email')?.patchValue(this.user.email);
    this.formGroup.get('eventId')?.patchValue(this.event.id);
    this.formGroup.get('price')?.patchValue(this.event.price);
  }

  saveData() {
    this.eventSignUpService.addSignUp(this.formGroup.value).subscribe((x) => {
      if (x == -1) this.toastr.error("Event doesn't exists!");
      else {
        this.toastr.success('Event signed up');
        this.router.navigateByUrl(`events/signUp/${this.id}`);
      }
    });
  }
}
