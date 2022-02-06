import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { EventVM } from '../events.model';

@Component({
  selector: 'app-events-modal',
  templateUrl: './events-modal.component.html',
  styleUrls: ['./events-modal.component.css']
})
export class EventsModalComponent implements OnInit {

  @Input() currentEvent!: EventVM;
  @Input() mapModal: any;
  @Output() clsModal= new EventEmitter<void>();
  @Output() blureOff = new EventEmitter<void>();



  constructor() {
   
  }

  test()
  {

  }
  ngOnInit(): void {
  }


  closeModal()
  {
    this.clsModal.emit();
    this.blureOff.emit();
  }
}
