import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageService } from 'src/app/helpers/image.service';
import { toBase64 } from 'src/app/helpers/toBase64';
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
  formData= new FormData();
  event : any;
  imgBase64: string ='';
  images: string[] = [];
  imgFiles: File[]= [];
  constructor(private es: EventsService, private builder: FormBuilder, private router: Router, private aRouter: ActivatedRoute,
              private imageService: ImageService) 
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
        this.saveData();
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

  saveData()
  {
      this.es.edit(this.id, this.groupData.value).subscribe(
        (value: any) => {
          this.imgFiles.forEach((img) => {
            console.log(img);
            this.formData.append('images', img);
          });
          this.imageService.addImages(this.id, this.formData, 'events').subscribe(
            () => {
              this.router.navigateByUrl('events');
              alert('Event edited!');
            }
          )
        });
   
  }

  change(event: any)
  {
     if(event.target.files.length > 0)
     {
        const img: File = event.target.files[0];
        this.imgFiles.push(img);
      toBase64(img).then((value: string)=>
      {
      this.imgBase64=value
      this.images.push(value);
      }
      );
    }
  }

  deleteImgPreview(i: number) {
    this.images.splice(i, 1);
    this.imgFiles.splice(i, 1);
  }

  deleteImageFromService(id: number)
  {
    this.imageService.deleteImage(id, 'events').subscribe(x=>
      {
        alert("Image deleted");
        this.router.navigate(['events']);

      })
  }
}
