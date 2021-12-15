import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { toBase64 } from 'src/app/helpers/toBase64';
import { SecurityService } from 'src/app/security/security.service';
import { AccommodationService } from '../accommodation.service';
import { ImageService } from '../image.service';

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

  //@ViewChild('img') img!: ElementRef;

  constructor(
    private _formBuilder: FormBuilder,
    private _accommodationService: AccommodationService,
    private _imageService: ImageService,
    private _router: Router,
    private _securityService: SecurityService
  ) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      name: ['', { validators: [Validators.required] }],
      price: ['', { validators: [Validators.required] }],
      hostId: [`${this._securityService.getFieldFromJWT('id')}`],
      location: this._formBuilder.group({
        country: ['', { validators: [Validators.required] }],
        city: ['', { validators: [Validators.required] }],
        address: ['', { validators: [Validators.required] }],
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
      }),
    });
    console.log(this.form.value);
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
    this.errors = [];

    this._accommodationService.add(this.form.value).subscribe(
      (res) => {
        this.imagesFiles.forEach((img) => {
          this.formData.append('images', img);
        });
        this._imageService.addImages(res as number, this.formData).subscribe(
          () => {
            this._router.navigate(['stays']);
          },
          (err) => (this.errors = parseWebAPiErrors(err))
        );
      },
      (err) => (this.errors = parseWebAPiErrors(err))
    );
  }
}
