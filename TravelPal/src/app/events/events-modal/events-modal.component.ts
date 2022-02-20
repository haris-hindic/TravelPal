import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { SecurityService } from 'src/app/security/security.service';
import { EventVM } from '../events.model';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-modal',
  templateUrl: './events-modal.component.html',
  styleUrls: ['./events-modal.component.css'],
})
export class EventsModalComponent implements OnInit {
  @Input() currentEvent!: EventVM;
  @Input() mapModal: any;
  @Output() clsModal = new EventEmitter<void>();
  @Output() blureOff = new EventEmitter<void>();

  constructor(public securityService: SecurityService) {}

  ngOnInit(): void {}

  closeModal() {
    this.clsModal.emit();
    this.blureOff.emit();
  }


}
