import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { EventsSignUpService } from '../events-sign-up.service';
import { EventSignUpVM } from '../eventsSignUp.model';

@Component({
  selector: 'app-events-sign-up-list',
  templateUrl: './events-sign-up-list.component.html',
  styleUrls: ['./events-sign-up-list.component.css'],
})
export class EventsSignUpListComponent implements OnInit {
  eventSignUps!: EventSignUpVM[];
  eventId!: number;

  // pagination
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private eventSignUpService: EventsSignUpService,
    private securityService: SecurityService,
    private toastr: ToastrService,
    private aRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.aRoute.params.subscribe((params) => {
      this.eventId = +params['eventId'];
      console.log(this.eventId);
      this.loadData();
    });
  }

  loadData() {
    this.eventSignUpService
      .getAllHostedSignUps(
        this.securityService.getFieldFromJWT('id'),
        this.eventId
      )
      .subscribe((x) => {
        this.eventSignUps = x.result;
        this.pagination = x.pagination;
        console.log(this.eventSignUps);
      });
  }

  onChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  cancelSignUp(id: number) {
    this.eventSignUpService.cancelSignUp(id).subscribe(() => {
      this.toastr.error('EventSignUp Cancelled!');
      this.loadData();
    });
  }
}
