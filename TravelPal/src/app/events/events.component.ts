import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Pagination } from '../shared/models/pagination';
import { EventVM } from './events.model';
import { EventsService } from './events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
})
export class EventsComponent implements OnInit {
  events: any;
  currentEvent!: any;
  eventsLoad: boolean = false;
  @Input() coverImage: any;
  groupData!: FormGroup;
  blure: any;
  disableMapBlure = false;

  // pagination
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private eventService: EventsService,
    private groupBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.loadData();

    this.groupData = this.groupBuilder.group({
      location: [''],
      from: [''],
      to: [''],
    });
  }

  loadData() {
    this.eventService.get(this.pageNumber, this.pageSize).subscribe((e) => {
      this.events = e.result;
      this.pagination = e.pagination;
      this.eventsLoad = true;
    });
  }

  searchEvents() {
    this.eventService.search(this.groupData?.value, this.pageNumber, this.pageSize).subscribe((e) => {
      this.events = e.result;
      this.pagination = e.pagination;
    });
  }

  blureOff() {
    this.blure = false;
    this.disableMapBlure = true;
  }

  selectEvent(s: EventVM) {
    this.currentEvent = s;
    this.blure = true;
    this.disableMapBlure = true;
  }

  eventDetails(event: EventVM) {
    console.log(event);
    this.selectEvent(event);
  }

  onChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.searchEvents();
  }
}
