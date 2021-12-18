import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageService } from 'src/app/helpers/image.service';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-details',
  templateUrl: './events-details.component.html',
  styleUrls: ['./events-details.component.css']
})
export class EventsDetailsComponent implements OnInit {

  url = 'https://localhost:44325/api/Event';
  id: number = -1;
  event: any;
  eventLoad = false;

  constructor(private http: HttpClient, private activeRouter: ActivatedRoute, private es: EventsService, private route: Router,
              private imageService: ImageService) { }

  ngOnInit(): void {
    this.activeRouter.params.subscribe((parameter) =>
      {
        this.id=parameter.id;
      }
    );

    this.es.getSpecific(this.id).subscribe(e=>
      {
        this.event=e;
        this.eventLoad=true;
      });

  }
  
  deleteEvent(id: number)
  {
    this.es.delete(id).subscribe(x=>
      {
        alert('Event deleted!');
        this.route.navigate(['events']);
      });
  }


}
