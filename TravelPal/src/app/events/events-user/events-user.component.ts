import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
  events!: EventVM[];
  page!: number;
  currentEvent!: any;
  IsEventsLoaded: boolean = false; 
  

  constructor(public securityService: SecurityService, private eventService: EventsService,
      private toastr: ToastrService) { }

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
        console.log(this.events);
        this.IsEventsLoaded = true;
      })
  }

  deleteEvent(id: number)
  {
    this.eventService.delete(id).subscribe((x)=> this.loadEvents());
    console.log(this.events);
    this.toastr.error("Event deleted");
  }
  

}
