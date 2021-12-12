import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventCreationVM, EventVM } from '../events.model';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-edit',
  templateUrl: './events-edit.component.html',
  styleUrls: ['./events-edit.component.css']
})
export class EventsEditComponent implements OnInit {

  id!: number;
  groupData!: FormGroup;
  formData!: FormData;
  event : any;
  constructor(private es: EventsService, private builder: FormBuilder, private router: Router, private aRouter: ActivatedRoute) 
    { }

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

    this.aRouter.params.subscribe((e)=>
    {
      this.id=e.id;
    });

    this.es.getSpecific(this.id).subscribe((e)=>
    {
      this.fillInputs(e);
      this.event = e;
    })



  }

  EditData()
  {
    this.es.edit(this.id, this.groupData.value).subscribe(e=>
      {
        alert('Event edited');
        this.router.navigate(['events']);
      })
  }

  fillInputs(value: any)
  {
    this.groupData.get('name')?.patchValue(value.name);
    this.groupData.get('price')?.patchValue(value.price);
    this.groupData.get('eventdescription')?.patchValue(value.eventdescription);
    this.groupData.get('date')?.patchValue(value.date);
    this.groupData.get('duration')?.patchValue(value.duration);
    this.groupData.get('locationvm')?.get('country')?.patchValue(value.locationVM.country);
    this.groupData.get('locationvm')?.get('city')?.patchValue(value.locationVM.city);
    this.groupData.get('locationvm')?.get('address')?.patchValue(value.locationVM.address);
    this.groupData.get('eventdescription')?.patchValue(value.eventDescription);
  }
}
