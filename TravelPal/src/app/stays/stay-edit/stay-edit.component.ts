import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { toBase64 } from 'src/app/helpers/toBase64';
import { Image } from 'src/app/shared/models/image.model';
import { AccommodationService } from '../accommodation.service';
import { ImageService } from 'src/app/helpers/image.service';
import { AccommodationVM } from '../stays.model';
import { country, city } from 'src/app/shared/models/location.model';
import { CountryCityService } from 'src/app/shared/country-city.service';
import { SecurityService } from 'src/app/security/security.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-stay-edit',
  templateUrl: './stay-edit.component.html',
  styleUrls: ['./stay-edit.component.css'],
})
export class StayEditComponent implements OnInit {
  form!: FormGroup;
  stay!: AccommodationVM;

  images: Image[] = [];
  formData: FormData = new FormData();
  newImages: string[] = [];
  newImageFiles: File[] = [];

  countries!: country[];
  cities!: city[];

  errors: string[] = [];

  constructor(
    private _route: ActivatedRoute,
    private _accommodationService: AccommodationService,
    private _imageService: ImageService,
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _countryCity: CountryCityService,
    private _securityService: SecurityService,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this._accommodationService
      .ownerShip(
        this._securityService.getFieldFromJWT('id'),
        this._route.snapshot.params['id']
      )
      .subscribe((x) => {
        if (x == false) {
          this._toastr.error('Error!');
          this._router.navigate([
            `/user-stays/${this._securityService.getFieldFromJWT('id')}`,
          ]);
        }
      });

    this.form = this._formBuilder.group({
      name: ['', { validators: [Validators.required] }],
      price: ['', { validators: [Validators.required] }],
      description: ['', { validators: [Validators.required] }],
      rooms: ['', { validators: [Validators.required] }],
      capacity: ['', { validators: [Validators.required] }],
      location: this._formBuilder.group({
        id: 0,
        country: ['', { validators: [Validators.required] }],
        cityId: ['', { validators: [Validators.required] }],
        address: ['', { validators: [Validators.required] }],
        latitude: ['', { validators: [Validators.required] }],
        longitude: ['', { validators: [Validators.required] }],
      }),
      accommodationDetails: this._formBuilder.group({
        accommodationDetailsId: 0,
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

    this.loadData();
  }

  loadData() {
    this._route.params.subscribe((params) => {
      this._accommodationService.getById(params.id).subscribe(
        (data: AccommodationVM) => {
          this.patchValues(data);
          this.images = data.images;
          this.stay = data;
          this._countryCity.getCountries().subscribe((cntrs) => {
            this.countries = cntrs;

            this._countryCity
              .getCitiesByCountry(this.stay.location.countryId)
              .subscribe((c) => {
                const index = c.findIndex(
                  (x) => x.id == this.stay.location.cityId
                );
                c.splice(index, 1);
                this.cities = c;
              });
          });
        },
        (err) => {
          this._toastr.error(err.error);
          this._router.navigate([
            `/user-stays/${this._securityService.getFieldFromJWT('id')}`,
          ]);
        }
      );
    });
  }

  loadImages() {
    this._route.params.subscribe((params) => {
      this._accommodationService
        .getById(params.id)
        .subscribe((data: AccommodationVM) => {
          this.images = data.images;
        });
    });
  }

  saveChanges() {
    this.errors = [];
    this._accommodationService
      .update(this._route.snapshot.params.id, this.form.value)
      .subscribe(
        () => {
          if (this.newImageFiles.length > 0) {
            this.newImageFiles.forEach((img) => {
              this.formData.append('images', img);
            });
            this._imageService
              .addImages(
                this._route.snapshot.params.id as number,
                this.formData,
                'accommodations'
              )
              .subscribe(
                () => {
                  this._router.navigate(['stays']);
                },
                (err: any) => {
                  this.errors = parseWebAPiErrors(err);
                }
              );
          } else {
            this._router.navigate(['stays']);
          }
        },
        (err) => {
          this.errors = parseWebAPiErrors(err);
        }
      );
  }

  deleteImage(id: number) {
    this._imageService
      .deleteImage(id, 'accommodations')
      .subscribe(() => this.loadImages());
  }

  imageSelected(event: any) {
    if (event.target.files.length > 0) {
      this.newImageFiles.push(event.target.files[0]);
      toBase64(event.target.files[0]).then((img) => {
        this.newImages.push(img);
      });
    }
  }

  mapClicked(event: { lat: number; lng: number }) {
    this.form.get('location')?.get('latitude')?.patchValue(event.lat);
    this.form.get('location')?.get('longitude')?.patchValue(event.lng);
  }

  deleteNewImage(i: number) {
    this.newImages.splice(i, 1);
    this.newImageFiles.splice(i, 1);
  }

  patchValues(data: AccommodationVM) {
    this.form.get('name')?.patchValue(data.name);
    this.form.get('price')?.patchValue(data.price);
    this.form.get('description')?.patchValue(data.description);
    this.form.get('rooms')?.patchValue(data.rooms);
    this.form.get('capacity')?.patchValue(data.capacity);
    this.form.get('location')?.get('id')?.patchValue(data.location.id);
    this.form
      .get('location')
      ?.get('country')
      ?.patchValue(data.location.country);
    this.form.get('location')?.get('cityId')?.patchValue(data.location.cityId);
    this.form
      .get('location')
      ?.get('address')
      ?.patchValue(data.location.address);
    this.form
      .get('location')
      ?.get('latitude')
      ?.patchValue(data.location.latitude);
    this.form
      .get('location')
      ?.get('longitude')
      ?.patchValue(data.location.longitude);
    this.form
      .get('accommodationDetails')
      ?.get('accommodationDetailsId')
      ?.patchValue(data.accommodationDetails.accommodationDetailsId);
    this.form
      .get('accommodationDetails')
      ?.get('parking')
      ?.patchValue(data.accommodationDetails.parking);
    this.form
      .get('accommodationDetails')
      ?.get('wiFi')
      ?.patchValue(data.accommodationDetails.wiFi);
    this.form
      .get('accommodationDetails')
      ?.get('shower')
      ?.patchValue(data.accommodationDetails.shower);
    this.form
      .get('accommodationDetails')
      ?.get('minibar')
      ?.patchValue(data.accommodationDetails.minibar);
    this.form
      .get('accommodationDetails')
      ?.get('airConditioning')
      ?.patchValue(data.accommodationDetails.airConditioning);
    this.form
      .get('accommodationDetails')
      ?.get('safe')
      ?.patchValue(data.accommodationDetails.safe);
    this.form
      .get('accommodationDetails')
      ?.get('dryer')
      ?.patchValue(data.accommodationDetails.dryer);
    this.form
      .get('accommodationDetails')
      ?.get('flatScreenTV')
      ?.patchValue(data.accommodationDetails.flatScreenTV);
    this.form
      .get('accommodationDetails')
      ?.get('bbq')
      ?.patchValue(data.accommodationDetails.bbq);
    this.form
      .get('accommodationDetails')
      ?.get('refrigerator')
      ?.patchValue(data.accommodationDetails.refrigerator);
    this.form
      .get('accommodationDetails')
      ?.get('balcony')
      ?.patchValue(data.accommodationDetails.balcony);
    this.form
      .get('accommodationDetails')
      ?.get('mosquitoNet')
      ?.patchValue(data.accommodationDetails.mosquitoNet);
    this.form
      .get('accommodationDetails')
      ?.get('cancellation')
      ?.patchValue(data.accommodationDetails.cancellation);
    this.form
      .get('accommodationDetails')
      ?.get('houseRules')
      ?.patchValue(data.accommodationDetails.houseRules);
  }

  changed() {
    this.form.get('location.city')?.reset();
    this.form.get('location.city')?.disable();
    const country = this.form.get('location')?.get('country')?.value;
    this._countryCity.getCitiesByCountry(country).subscribe((c) => {
      this.cities = c;
      this.form.get('location.city')?.enable();
    });
  }
}
