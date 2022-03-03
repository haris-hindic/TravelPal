import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { EventVM } from 'src/app/events/events.model';
import { EventsService } from 'src/app/events/events.service';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {
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
    private toastr: ToastrService,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.name = this.securityService.getFieldFromJWT('userName');
    this.id = this.securityService.getFieldFromJWT('id');
    this.loadEvents();
  }

  loadEvents() {
    this.adminService.getEvents(this.pageNumber, this.pageSize).subscribe((e) => {
      this.events = e.result;
      this.pagination = e.pagination;
      this.IsEventsLoaded = true;
    });
  }

  deleteEvent(id: number) {
    this.adminService.deleteEvent(id).subscribe((x) => 
    {
    this.loadEvents();
    this.toastr.error('Event deleted');
    });
  }
  
  onChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadEvents();
  }
}
