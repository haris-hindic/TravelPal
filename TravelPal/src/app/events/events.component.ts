import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EventsService } from './events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
})
export class EventsComponent implements OnInit {
  events: any;
  currentEvent!: any;
  eventsLoad: boolean = false;
  @Input() coverImage: any;
  groupData!: FormGroup;
  
  constructor(private eventService: EventsService, private groupBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.loadList();

    this.groupData = this.groupBuilder.group(
      {
        location: [''],
        from: [''],
        to: [''],
      });
  }

  loadList()
  {
    this.eventService.get().subscribe((e) => {
      this.events = e;
      console.log(this.events);
      this.eventsLoad = true;
    });
  }
  searchEvents()
  {
   
   this.eventService.search(this.groupData?.value).subscribe((e:any) => {
     this.events = e;
   });
 }
  
  
}