import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  @Input() test! : any;
  @Input() isEvents: boolean = false;
  @Output() blure = new EventEmitter<void>();
  @Output() blureOff = new EventEmitter<void>();
   test2!: any;
  currentEvent!: EventVM;
  showModal!: any;

  constructor() {
    this.test2=this.test;

    console.log(this.test2);

    if(this.test2==true)
    { 
      this.blureOff.emit();
    }


  }

  ngOnInit(): void {}

  setEvent(event: EventVM)
  {
    this.currentEvent = event;
    this.showModal = true;
    this.blure.emit();
  }
}
