import { MouseEvent } from '@agm/core';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})
export class MapComponent implements OnInit {
  @Output() onClicked = new EventEmitter<{ lat: number; lng: number }>();
  @Input() coordinates!: { lat: number; lng: number };
  @Input() readonly: boolean = false;
  @Input() mapIconURL!: string;

  constructor() {}

  ngOnInit(): void {
    if (!this.coordinates) {
      this.coordinates = { lat: 0, lng: 0 };
    }
  }

  mapClicked(event: MouseEvent) {
    if (!this.readonly) {
      this.coordinates = { lat: event.coords.lat, lng: event.coords.lng };
      this.onClicked.emit(this.coordinates);
    }
  }
}
