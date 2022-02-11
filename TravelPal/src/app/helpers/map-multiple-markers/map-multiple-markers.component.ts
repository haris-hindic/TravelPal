import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EventVM } from 'src/app/events/events.model';
import {
  AccommodationBasicVM,
  AccommodationVM,
} from 'src/app/stays/stays.model';

@Component({
  selector: 'app-map-multiple-markers',
  templateUrl: './map-multiple-markers.component.html',
  styleUrls: ['./map-multiple-markers.component.css'],
})
export class MapMultipleMarkersComponent implements OnInit {
  @Input() stays!: AccommodationBasicVM[];
  @Input() events!: EventVM[];
  @Input() isEvents: boolean = false;
  @Output() modalEvent = new EventEmitter<EventVM>();
  showModal!: any;

  constructor() {}

  ngOnInit(): void {}

  setEvent(event: EventVM) {
    this.showModal = true;
    this.modalEvent.emit(event);
  }
}
