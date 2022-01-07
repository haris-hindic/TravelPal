import { Component, Input, OnInit } from '@angular/core';
import { EventVM } from 'src/app/events/events.model';
import { AccommodationVM } from 'src/app/stays/stays.model';

@Component({
  selector: 'app-map-multiple-markers',
  templateUrl: './map-multiple-markers.component.html',
  styleUrls: ['./map-multiple-markers.component.css'],
})
export class MapMultipleMarkersComponent implements OnInit {
  @Input() stays!: AccommodationVM[];
  @Input() events!: EventVM[];
  @Input() isEvents: boolean = false;

  constructor() {}

  ngOnInit(): void {}
}
