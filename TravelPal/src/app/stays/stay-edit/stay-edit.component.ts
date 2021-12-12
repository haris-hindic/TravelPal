import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { toBase64 } from 'src/app/helpers/toBase64';
import { Image } from 'src/app/models/image.model';
import { AccommodationService } from '../accommodation.service';
import { ImageService } from '../image.service';
import { AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-edit',
  templateUrl: './stay-edit.component.html',
  styleUrls: ['./stay-edit.component.css'],
})
export class StayEditComponent implements OnInit {
  form!: FormGroup;
  images: Image[] = [];
  formData: FormData = new FormData();
  newImages: string[] = [];
  newImageFiles: File[] = [];

  errors: string[] = [];

  constructor(
    private _route: ActivatedRoute,
    private _accommodationService: AccommodationService,
    private _imageService: ImageService,
    private _formBuilder: FormBuilder,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      name: ['', { validators: [Validators.required] }],
      price: ['', { validators: [Validators.required] }],
      location: this._formBuilder.group({
        id: 0,
        country: ['', { validators: [Validators.required] }],
        city: ['', { validators: [Validators.required] }],
        address: ['', { validators: [Validators.required] }],
      }),
      accommodationDetails: this._formBuilder.group({
        id: 0,
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
      }),
    });
    this.loadData();
  }

  loadData() {
    this._route.params.subscribe((params) => {
      this._accommodationService
        .getById(params.id)
        .subscribe((data: AccommodationVM) => {
          this.patchValues(data);
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
                this.formData
              )
              .subscribe(
                () => {
                  this._router.navigate(['stays']);
                },
                (err) => {
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
    this._imageService.deleteImage(id).subscribe(() => this.loadData());
  }

  imageSelected(event: any) {
    if (event.target.files.length > 0) {
      this.newImageFiles.push(event.target.files[0]);
      toBase64(event.target.files[0]).then((img) => {
        this.newImages.push(img);
      });
    }
  }

  deleteNewImage(i: number) {
    this.newImages.splice(i, 1);
    this.newImageFiles.splice(i, 1);
  }

  patchValues(data: AccommodationVM) {
    this.form.get('name')?.patchValue(data.name);
    this.form.get('price')?.patchValue(data.price);
    this.form.get('location')?.get('id')?.patchValue(data.location.id);
    this.form
      .get('location')
      ?.get('country')
      ?.patchValue(data.location.country);
    this.form.get('location')?.get('city')?.patchValue(data.location.city);
    this.form
      .get('location')
      ?.get('address')
      ?.patchValue(data.location.address);
    this.form
      .get('accommodationDetails')
      ?.get('id')
      ?.patchValue(data.accommodationDetails.id);
    this.form
      .get('accommodationDetails')
      ?.get('parking')
      ?.patchValue(data.accommodationDetails.parking);
    this.form
      .get('accommodationDetails')
      ?.get('wiFi')
      ?.patchValue(data.accommodationDetails.wifi);
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
  }
}
