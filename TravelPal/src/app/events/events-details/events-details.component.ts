import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

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

  constructor(private http: HttpClient, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.router.params.subscribe(
      (parameter) =>
      {
        this.id=parameter.id;
      }
    )
    this.http.get(this.url + '/' + this.id).subscribe(
      (parameter) =>
      {
        this.event=parameter;
        this.eventLoad=true;
      }
    )
  }

}
