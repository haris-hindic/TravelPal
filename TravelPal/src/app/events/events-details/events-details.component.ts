import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ImageService } from 'src/app/helpers/image.service';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-details',
  templateUrl: './events-details.component.html',
  styleUrls: ['./events-details.component.css']
})
export class EventsDetailsComponent implements OnInit {

  url = 'https://localhost:44325/api/Event';
  event: any;
  eventLoad = false;

  constructor(private http: HttpClient, private activeRouter: ActivatedRoute, private es: EventsService, private route: Router,
              private imageService: ImageService, private toastr: ToastrService, private aRoute: ActivatedRoute) { }

  ngOnInit(): void {
    
    this.loadData();
  }
  
  loadData()
  {
    this.aRoute.params.subscribe((params) => {
      this.es.getSpecific(params.id).subscribe((value) => 
      {
          this.event = value;
          this.eventLoad=true;
      });
    });
  }
  deleteEvent(id: number)
  {
    this.es.delete(id).subscribe(x=>
      {
        this.toastr.error("Event deleted!")
        this.route.navigate(['events']);
      });
  }
}
