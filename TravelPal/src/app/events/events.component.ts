import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
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
  constructor(private http: HttpClient, private es: EventsService, private route: Router) {}

  ngOnInit(): void {
    this.loadList();
  }

  saveEvent()
  {
    this.es.post({id:142, name: 'Ermin', price:24, date: '2021-12-12', duration:'2', eventDescription: 'Ajmo', locationVM: {
      id: 1, country:'BiH', city:'Mostar', address:'MostarskaBB'}}).subscribe(x=>
      {
        alert("Uspjesno dodat event");
        this.loadList();
      });
  }

  loadList()
  {
    this.es.get().subscribe((e) => {
      this.events = e;
    });
  }
}