import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-user',
  templateUrl: './events-user.component.html',
  styleUrls: ['./events-user.component.css']
})
export class EventsUserComponent implements OnInit {

  name: string = '-1';
  id: string =  '';

  constructor(public securityService: SecurityService, activeRoute: ActivatedRoute, private eventService: EventsService) { }

  ngOnInit(): void {
    this.name = this.securityService.getFieldFromJWT('userName');
    this.id = this.securityService.getFieldFromJWT('id');

    this.loadEvents();
  }
  
  loadEvents(){}

}
