import { Component, Input, OnInit } from '@angular/core';
import { EventVM } from 'src/app/events/events.model';
import { AccommodationVM } from 'src/app/stays/stays.model';

@Component({
  selector: 'app-map-multiple-markers',
  templateUrl: './map-multiple-markers.component.html',
  styleUrls: ['./map-multiple-markers.component.css'],
})
export class MapMultipleMarkersComponent implements OnInit {
  markers: { lat: number; lng: number }[] = [
    { lat: 43.8510131277294, lng: 18.407407890579037 },
    { lat: 43.9510131277294, lng: 18.407407890579037 },
    { lat: 43.2510131277294, lng: 18.407407890579037 },
    { lat: 43.3510131277294, lng: 18.407407890579037 },
  ];

  @Input() stays!: AccommodationVM[];
  @Input() events!: EventVM[];
  @Input() isEvents : boolean = false;

  constructor() {}

  ngOnInit(): void {}
}
