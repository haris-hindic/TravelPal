import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-creation',
  templateUrl: './events-creation.component.html',
  styleUrls: ['./events-creation.component.css']
})
export class EventsCreationComponent implements OnInit {

  groupData!: FormGroup;
  formData!: FormData;


  constructor(private es: EventsService, private builder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.groupData = this.builder.group(
      {
        name: ['', {validators: [Validators.required]}],
        price: ['',{validators: [Validators.required]}],
        date: ['', {validators: [Validators.required]}],
        duration: ['', {validators: [Validators.required]}],
        eventdescription: ['', {validators: [Validators.required]}],
        locationvm: this.builder.group(
          {
           country: ['', {validators: [Validators.required]}],
           city: ['', {validators: [Validators.required]}],
           address: ['', {validators: [Validators.required]}],
        }),
        }
    );
  }

  saveData()
  {
    this.es.post(this.groupData.value).subscribe(
      e=>
      {
        alert("Event dodan!");
        this.router.navigate(['events']);
      }
    )
  }
}
