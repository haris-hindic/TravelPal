import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { EventVM } from '../events.model';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-user',
  templateUrl: './events-user.component.html',
  styleUrls: ['./events-user.component.css'],
})
export class EventsUserComponent implements OnInit {
  name: string = '-1';
  id: string = '-1';
  events!: EventVM[];
  page!: number;
  currentEvent!: any;
  IsEventsLoaded: boolean = false;

  // pagination
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    public securityService: SecurityService,
    private eventService: EventsService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.name = this.securityService.getFieldFromJWT('userName');
    this.id = this.securityService.getFieldFromJWT('id');

    this.loadEvents();
  }

  loadEvents() {
    this.eventService.getUserEvents(this.id, this.pageNumber, this.pageSize).subscribe((e) => {
      this.events = e.result;
      this.pagination = e.pagination;
      this.IsEventsLoaded = true;
    });
  }

  deleteEvent(id: number) {
    this.eventService.delete(id).subscribe((x) => this.loadEvents());
    console.log(this.events);
    this.toastr.error('Event deleted');
  }

  
  onChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadEvents();
  }
}
