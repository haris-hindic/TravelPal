import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ImageService } from 'src/app/helpers/image.service';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { toBase64 } from 'src/app/helpers/toBase64';
import { Image } from 'src/app/shared/models/image.model';
import { EventCreationVM, EventVM } from '../events.model';
import { EventsService } from '../events.service';

@Component({
  selector: 'app-events-edit',
  templateUrl: './events-edit.component.html',
  styleUrls: ['./events-edit.component.css'],
})
export class EventsEditComponent implements OnInit {
  id!: number;
  groupData!: FormGroup;
  formData = new FormData();
  event!: EventVM;
  errors: string[] = [];
  imgBase64: string = '';
  currentImages: Image[] = [];
  images: string[] = [];
  imgFiles: File[] = [];
  constructor(
    private es: EventsService,
    private builder: FormBuilder,
    private router: Router,
    private aRouter: ActivatedRoute,
    private imageService: ImageService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.groupData = this.builder.group({
      name: [
        '',
        { validators: [Validators.required, Validators.minLength(3)] },
      ],
      price: ['', { validators: [Validators.required] }],
      date: ['', { validators: [Validators.required] }],
      duration: ['', { validators: [Validators.required] }],
      eventdescription: ['', { validators: [Validators.required] }],
      locationvm: this.builder.group({
        country: ['', { validators: [Validators.required] }],
        city: ['', { validators: [Validators.required] }],
        address: ['', { validators: [Validators.required] }],
        longitude: [0,{validators: [Validators.required]}],
        latitude: [0,{validators: [Validators.required]}]
      }),
    });
    this.loadData();
  }
  loadData()
  {
    this.aRouter.params.subscribe((params) => {
      this.es.getSpecific(params.id).subscribe((e: any) => {
        this.fillInputs(e);
        this.currentImages = e.images;
      });
    });
    console.log(event);
  }

  EditData() {
    this.saveData();
  }

  fillInputs(value: any) {
    this.groupData.get('name')?.patchValue(value.name);
    this.groupData.get('price')?.patchValue(value.price);
    this.groupData.get('eventdescription')?.patchValue(value.eventdescription);
    this.groupData.get('date')?.patchValue(value.date);
    this.groupData.get('duration')?.patchValue(value.duration);
    this.groupData
      .get('locationvm')
      ?.get('country')
      ?.patchValue(value.locationVM.country);
    this.groupData
      .get('locationvm')
      ?.get('city')
      ?.patchValue(value.locationVM.city);
    this.groupData
      .get('locationvm')
      ?.get('address')
      ?.patchValue(value.locationVM.address);
    this.groupData.get('eventdescription')?.patchValue(value.eventDescription);
    this.groupData.get('locationvm.latitude')?.patchValue(value.locationVM.latitude);
    this.groupData.get('locationvm.longitude')?.patchValue(value.locationVM.longitude);
  }

  saveData() {
    console.log(this.groupData.value);
    this.es
      .edit(this.aRouter.snapshot.params.id as number, this.groupData.value)
      .subscribe(
        () => {
          if (this.imgFiles.length > 0) {
            this.imgFiles.forEach((img) => {
              console.log(img);
              this.formData.append('images', img);
            });
            this.imageService
              .addImages(
                this.aRouter.snapshot.params.id as number,
                this.formData,
                'events'
              )
               .subscribe(() => {});

          }
          this.router.navigateByUrl('events');
          this.toastr.info('Event edited!');
        },
        (err) => {
          this.errors = parseWebAPiErrors(err);
        }
      );
  }

  change(event: any) {
    if (event.target.files.length > 0) {
      const img: File = event.target.files[0];
      this.imgFiles.push(img);
      toBase64(img).then((value: string) => {
        this.imgBase64 = value;
        this.images.push(value);
      });
    }
  }

  deleteImage(id: number) {
    this.imageService.deleteImage(id, 'events').subscribe((a) => {
      this.toastr.error('Image deleted!');
      this.loadData();

    });
  }

  deleteImg(i: number) {
    this.images.splice(i, 1);
    this.imgFiles.splice(i, 1);
  }

  map(event: { lat: number; lng: number }) {
    console.log(event);
    this.groupData.get('locationvm')?.get('latitude')?.patchValue(event.lat);
    this.groupData.get('locationvm')?.get('longitude')?.patchValue(event.lng);
  }
}
