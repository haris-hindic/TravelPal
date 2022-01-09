import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private es: EventsService) {}

  ngOnInit(): void {
    this.loadList();
  }

  loadList()
  {
    this.es.get().subscribe((e) => {
      this.events = e;
      console.log(this.events);
      this.eventsLoad = true;
    });
  }

  
  
}