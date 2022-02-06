import {
  Component,
  ElementRef,
  HostListener,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { toBase64 } from 'src/app/helpers/toBase64';
import { SecurityService } from 'src/app/security/security.service';
import { AccommodationService } from '../accommodation.service';
import { ImageService } from 'src/app/helpers/image.service';
import { ToastrService } from 'ngx-toastr';
import { city, country } from 'src/app/shared/models/location.model';
import { CountryCityService } from 'src/app/shared/country-city.service';
import { AccommodationCreationVM } from '../stays.model';

@Component({
  selector: 'app-stay-create',
  templateUrl: './stay-create.component.html',
  styleUrls: ['./stay-create.component.css'],
})
export class StayCreateComponent implements OnInit {
  form!: FormGroup;
  formData: FormData = new FormData();

  images: string[] = [];
  imagesFiles: File[] = [];

  errors: string[] = [];

  cities!: city[];
  countries!: country[];

  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.form.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private _formBuilder: FormBuilder,
    private _accommodationService: AccommodationService,
    private _imageService: ImageService,
    private _router: Router,
    private _securityService: SecurityService,
    private toastr: ToastrService,
    private _countryCity: CountryCityService
  ) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      name: ['', { validators: [Validators.required] }],
      price: ['', { validators: [Validators.required] }],
      rooms: ['', { validators: [Validators.required] }],
      description: ['', { validators: [Validators.required] }],
      capacity: ['', { validators: [Validators.required] }],
      hostId: [`${this._securityService.getFieldFromJWT('id')}`],
      location: this._formBuilder.group({
        //country: ['', { validators: [Validators.required] }],
        cityId: [
          { value: '', disabled: true },
          { validators: [Validators.required] },
        ],
        address: ['', { validators: [Validators.required] }],
        latitude: [0, { validators: [Validators.required] }],
        longitude: [0, { validators: [Validators.required] }],
      }),
      accommodationDetails: this._formBuilder.group({
        parking: false,
        wiFi: false,
        shower: false,
        minibar: false,
        airConditioning: false,
        safe: false,
        dryer: false,
        flatScreenTV: false,
        petFriendly: false,
        bbq: false,
        refrigerator: false,
        balcony: false,
        mosquitoNet: false,
        cancellation: ['', { validators: [Validators.required] }],
        houseRules: ['', { validators: [Validators.required] }],
      }),
    });

    this._countryCity.getCountries().subscribe((cntrs) => {
      this.countries = cntrs;
    });
  }

  mapClicked(event: { lat: number; lng: number }) {
    this.form.get('location')?.get('latitude')?.patchValue(event.lat);
    this.form.get('location')?.get('longitude')?.patchValue(event.lng);
  }

  imageSelected(event: any) {
    if (event.target.files.length > 0) {
      //this.formData.append('images', event.target.files[0]);
      this.imagesFiles.push(event.target.files[0]);
      toBase64(event.target.files[0]).then((img) => {
        this.images.push(img);
      });
    }
  }

  deleteImage(i: number) {
    this.images.splice(i, 1);
    this.imagesFiles.splice(i, 1);
  }

  saveChanges() {
    //console.log(this.form.value);
    this.errors = [];
    this._accommodationService
      .add(this.form.value as AccommodationCreationVM)
      .subscribe(
        (res) => {
          this.imagesFiles.forEach((img) => {
            this.formData.append('images', img);
          });
          this._imageService
            .addImages(res as number, this.formData, 'accommodations')
            .subscribe(
              () => {
                this.form.reset();
                this._router.navigate(['stays']);
                this.toastr.success('Stay created!');
              },
              (err) => (this.errors = parseWebAPiErrors(err))
            );
        },
        (err) => (this.errors = parseWebAPiErrors(err))
      );
  }
  changed(countryId: any) {
    this.form.get('location.cityId')?.reset();
    this.form.get('location.cityId')?.disable();

    this._countryCity.getCitiesByCountry(countryId).subscribe((c) => {
      this.cities = c;
      this.form.get('location.cityId')?.enable();
    });
  }
}
