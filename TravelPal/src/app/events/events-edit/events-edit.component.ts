import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ImageService } from 'src/app/helpers/image.service';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { toBase64 } from 'src/app/helpers/toBase64';
import { CountryCityService } from 'src/app/shared/country-city.service';
import { Image } from 'src/app/shared/models/image.model';
import { city, country } from 'src/app/shared/models/location.model';
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
  cities!: city[];
  countries!: country[];

  constructor(
    private es: EventsService,
    private builder: FormBuilder,
    private router: Router,
    private aRoute: ActivatedRoute,
    private imageService: ImageService,
    private toastr: ToastrService,
    private countryCity: CountryCityService
  ) {}

  ngOnInit(): void {
    this.groupData = this.builder.group({
      name: [
        '',
        { validators: [Validators.required, Validators.minLength(3)] },
      ],
      price: ['', { validators: [Validators.required] }],
      date: ['', { validators: [Validators.required] }],
      duration: [0, { validators: [Validators.required] }],
      eventdescription: ['', { validators: [Validators.required] }],
      locationvm: this.builder.group(
        {
          country: ['', { validators: [Validators.required] }],
          cityId:   [{ value: ''}, { validators: [Validators.required] }, ],
          address: ['', {validators: [Validators.required]}],
          longitude: [0,{validators: [Validators.required]}],
          latitude: [0,{validators: [Validators.required]}]
      }),
    });
    this.loadData();
  }
  loadData()
  {
    this.aRoute.params.subscribe((params) => {
      this.es.getSpecific(params.id).subscribe((e: any) => {
        this.fillInputs(e);
        this.event=e;
        console.log(e);
        this.currentImages = e.images;
        
        // Get countries
        this.countryCity
        .getCountries()
        .subscribe((country) => {
          console.log(country);
          const index = country.findIndex(
            (x) => x.id == this.event?.locationVM?.countryId
          );
          country.splice(index, 1);
          this.countries = country;
        });

        // Get cities
        this.countryCity
        .getCitiesByCountry(this.event.locationVM.countryId)
        .subscribe((city) => {
          const index = city.findIndex(
            (x) => x.id == this.event.locationVM.cityId
          );
          city.splice(index, 1);
          this.cities = city;
        });
      });
    });
    console.log(this.event?.locationVM?.countryId);
   
  }

  EditData() {
    this.saveData();
  }

  fillInputs(value: EventVM) {
    this.groupData.get('name')?.patchValue(value.name);
    this.groupData.get('price')?.patchValue(value.price);
    this.groupData.get('eventdescription')?.patchValue(value.eventDescription);
    this.groupData.get('date')?.patchValue(this.formatDate(new Date(value.date)));
    this.groupData.get('duration')?.patchValue(value.duration);
    this.groupData.get('locationvm.address')?.patchValue(value.locationVM.address);
    this.groupData.get('eventdescription')?.patchValue(value.eventDescription);
    this.groupData.get('locationvm.latitude')?.patchValue(value.locationVM.latitude);
    this.groupData.get('locationvm.longitude')?.patchValue(value.locationVM.longitude);
    this.groupData.get('locationvm')?.get('id')?.patchValue(value.locationVM.id);
    this.groupData.get('locationvm')?.get('country')?.patchValue(value.locationVM.country);
    this.groupData.get('locationvm')?.get('cityId')?.patchValue(value.locationVM.cityId);

    console.log("Country -> " + value.locationVM.country);
    console.log("CityId -> " + value.locationVM.city);
    console.log("Duration -> " + value.duration);


  }
    formatDate(date: Date) {
    let MM = '' + (date.getMonth()+1);
    let dd = '' + date.getDate();
    const yyyy = date.getFullYear();
    if (MM.length < 2) MM = '0' + MM;
    if (dd.length < 2) dd = '0' + dd;
    return [yyyy, MM, dd].join('-');
  }

  saveData() {

    this.es
      .edit(this.aRoute.snapshot.params.id as number, this.groupData.value)
      .subscribe(
        () => {
          if (this.imgFiles.length > 0) {
            this.imgFiles.forEach((img) => {
              console.log(img);
              this.formData.append('images', img);
            });
            this.imageService
              .addImages(this.aRoute.snapshot.params.id as number, this.formData,'events')
            
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

  changed(countryId: any) {
    this.groupData.get('locationvm.cityId')?.reset();
    this.groupData.get('locationvm.cityId')?.disable();

    this.countryCity.getCitiesByCountry(countryId).subscribe((c) => {
      this.cities = c;
      this.groupData.get('locationvm.cityId')?.enable();
    });
  }

}
