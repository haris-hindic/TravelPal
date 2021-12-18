import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, MinLengthValidator, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { toBase64 } from 'src/app/helpers/toBase64';
import { EventsService } from '../events.service';
import { ImageService } from 'src/app/helpers/image.service';   
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-events-creation',
  templateUrl: './events-creation.component.html',
  styleUrls: ['./events-creation.component.css']
})
export class EventsCreationComponent implements OnInit {

  groupData!: FormGroup;
  formData: FormData = new FormData();
  errors : string[] = [];
  imgBase64: string ='';
  images: string[] = [];
  imgFiles: File[]= [];


  constructor(private es: EventsService, private builder: FormBuilder, private router: Router, private is: ImageService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.groupData = this.builder.group(
      {
        name: ['', {validators: [Validators.required, Validators.minLength(3)]}],
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
        (res: any) => {
          this.imgFiles.forEach((img) => {
            console.log(img);
            this.formData.append('images', img);
          });
          this.is.addImages(res.id, this.formData, 'events').subscribe(
            () => {
              this.router.navigateByUrl('events');
              this.toastr.success("Event added!")
            },
            err=>
            {
              this.errors = parseWebAPiErrors(err);
            }
          )
          }, err=>
          {
            this.errors = parseWebAPiErrors(err);
          }
      );
  
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

  deleteImg(i: number) {
    this.images.splice(i, 1);
    this.imgFiles.splice(i, 1);
  }
}
