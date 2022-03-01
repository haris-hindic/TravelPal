import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { EventsSignUpService } from './events-sign-up.service';
import { EventSignUpVM } from './eventsSignUp.model';

@Component({
  selector: 'app-events-sign-up',
  templateUrl: './events-sign-up.component.html',
  styleUrls: ['./events-sign-up.component.css'],
})
export class EventsSignUpComponent implements OnInit {
  eventSignUps!: EventSignUpVM[];

  // pagination
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

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
        this.eventSignUps = x.result;
        this.pagination = x.pagination;
      });
  }

  cancelSignUp(id: number) {
    this.eventSignUpService.cancelSignUp(id).subscribe(() => {
      this.toastr.error('EventSignUp Cancelled!');
      this.getAll();
    });
  }
  onChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.getAll();
  }
}
