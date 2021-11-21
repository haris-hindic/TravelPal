import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TestService } from '../test.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  testing: any;
  constructor(private testService: TestService) { }

  ngOnInit(): void 
  {
    this.testService.ClickOnLocation();
    this.testService.autPut.subscribe((povratna) =>{
      console.log(povratna);
      this.testing=povratna; })
  }
}

  
    



