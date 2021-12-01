import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { toBase64 } from 'src/app/helpers/toBase64';
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

  constructor(
    private _formBuilder: FormBuilder,
    private _accommodationService: AccommodationService,
    private _imageService: ImageService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      name: ['', { validators: [Validators.required] }],
      price: ['', { validators: [Validators.required] }],
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
  }

  imageSelected(event: any) {
    if (event.target.files.length > 0) {
      this.formData.append('images', event.target.files[0]);

      toBase64(event.target.files[0]).then((img) => {
        this.images.push(img);
      });
    }
  }

  saveChanges() {
    this._accommodationService.add(this.form.value).subscribe((res) => {
      this._imageService
        .addImages(res as number, this.formData)
        .subscribe(() => {
          this._router.navigate(['stays']);
        });
    });
  }
}
