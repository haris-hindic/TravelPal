import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ImageService } from '../image.service';
import { AccommodationService } from '../accommodation.service';
import { AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-details',
  templateUrl: './stay-details.component.html',
  styleUrls: ['./stay-details.component.css'],
})
export class StayDetailsComponent implements OnInit {
  stay!: AccommodationVM;

  constructor(
    private _route: ActivatedRoute,
    private _accommodationService: AccommodationService,
    private _imageService: ImageService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._route.params.subscribe((params) => {
      this._accommodationService
        .getById(params.id)
        .subscribe((data: AccommodationVM) => {
          this.stay = data;
          console.log('data :>> ', data);
        });
    });
  }

  delete(id: number) {
    console.log(id);
    this._imageService.deleteImage(id).subscribe(() => this.loadData());
  }
}
