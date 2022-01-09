import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import { EventVM } from '../events.model';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-user',
  templateUrl: './events-user.component.html',
  styleUrls: ['./events-user.component.css']
})
export class EventsUserComponent implements OnInit {

  name: string = '-1';
  id: string =  '-1';
  events!: any;
  page!: number;
  currentEvent!: any;

  constructor(public securityService: SecurityService, activeRoute: ActivatedRoute, private eventService: EventsService) { }

  ngOnInit(): void {
    this.name = this.securityService.getFieldFromJWT('userName');
    this.id = this.securityService.getFieldFromJWT('id');

    this.loadEvents();
  }
  
  loadEvents()
  {
    this.eventService.getUserEvents(this.id).subscribe((e: any) =>
      {
        this.events = e;
      })
  }

  deleteEvent(id: number)
  {
    this.eventService.delete(id).subscribe(()=> this.loadEvents());
  }

}
