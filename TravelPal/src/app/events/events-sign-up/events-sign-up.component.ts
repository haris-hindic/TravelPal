import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { EventsSignUpService } from './events-sign-up.service';
import { EventSignUpVM } from './eventsSignUp.model';

@Component({
  selector: 'app-events-sign-up',
  templateUrl: './events-sign-up.component.html',
  styleUrls: ['./events-sign-up.component.css'],
})
export class EventsSignUpComponent implements OnInit {
  eventSignUps!: EventSignUpVM[];

  constructor(
    private eventSignUpService: EventsSignUpService,
    private securityService: SecurityService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.eventSignUpService
      .getAll(this.securityService.getFieldFromJWT('id'))
      .subscribe((x) => {
        this.eventSignUps = x;
      });
  }

  cancelSignUp(id: number) {
    this.eventSignUpService.cancelSignUp(id).subscribe(() => {
      this.toastr.error('EventSignUp Cancelled!');
      this.getAll();
    });
  }
}
