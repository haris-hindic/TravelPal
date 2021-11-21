import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  MojTest: string = 'https://localhost:44325/api/Location';
  
  get()
  {
    this.http.get(this.MojTest).subscribe((rez) =>
    {
      console.log(rez);
    })
  }

}
