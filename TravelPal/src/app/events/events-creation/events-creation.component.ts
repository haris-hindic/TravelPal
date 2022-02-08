import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { toBase64 } from 'src/app/helpers/toBase64';
import { EventsService } from '../events.service';
import { ImageService } from 'src/app/helpers/image.service';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { CountryCityService } from 'src/app/shared/country-city.service';
import { city, country } from 'src/app/shared/models/location.model';
import { EventVM } from '../events.model';

@Component({
  selector: 'app-events-creation',
  templateUrl: './events-creation.component.html',
  styleUrls: ['./events-creation.component.css'],
})
export class EventsCreationComponent implements OnInit {
  groupData!: FormGroup;
  formData: FormData = new FormData();
  errors: string[] = [];
  imgBase64: string = '';
  images: string[] = [];
  imgFiles: File[] = [];
  cities!: city[];
  countries!: country[];
  test!: string;

  constructor(
    private eventService: EventsService,
    private builder: FormBuilder,
    private router: Router,
    private imageService: ImageService,
    private toastr: ToastrService,
    private securityService: SecurityService,
    private countryCity: CountryCityService
  ) {}

  ngOnInit(): void {
    this.groupData = this.builder.group({
      hostId: [`${this.securityService.getFieldFromJWT('id')}`],
      name: [
        '',
        { validators: [Validators.required, Validators.minLength(3)] },
      ],
      price: ['', { validators: [Validators.required] }],
      date: ['', { validators: [Validators.required] }],
      duration: [0, { validators: [Validators.required] }],
      eventdescription: ['', { validators: [Validators.required] }],
      locationvm: this.builder.group({
        cityId: [
          { value: '', disabled: true },
          { validators: [Validators.required] },
        ],
        address: ['', { validators: [Validators.required] }],
        longitude: [0, { validators: [Validators.required] }],
        latitude: [0, { validators: [Validators.required] }],
      }),
    });

    this.countryCity.getCountries().subscribe((e) => {
      this.countries = e;
    });
  }

  saveData() {
    this.eventService.post(this.groupData.value).subscribe(
      (id) => {
        this.imgFiles.forEach((img) => {
          console.log(img);
          this.formData.append('images', img);
        });
        this.imageService
          .addImages(id as number, this.formData, 'events')
          .subscribe(
            () => {
              this.router.navigateByUrl('events');
              this.toastr.success('Event added!');
            },
            (err) => {
              this.errors = parseWebAPiErrors(err);
            }
          );
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

  deleteImg(i: number) {
    this.images.splice(i, 1);
    this.imgFiles.splice(i, 1);
  }

  map(event: { lat: number; lng: number }) {
    console.log(event);
    this.groupData.get('locationvm')?.get('latitude')?.patchValue(event.lat);
    this.groupData.get('locationvm')?.get('longitude')?.patchValue(event.lng);
    console.log(this.groupData.value);
  }

  changed(countryId: any) {
    console.log(countryId);
    this.groupData.get('locationvm.cityId')?.reset();
    this.groupData.get('locationvm.cityId')?.disable();

    this.countryCity.getCitiesByCountry(countryId).subscribe((c) => {
      this.cities = c;
      console.log(c);
      this.groupData.get('locationvm.cityId')?.enable();
    });
  }
}
