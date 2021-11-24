import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-details',
  templateUrl: './stay-details.component.html',
  styleUrls: ['./stay-details.component.css'],
})
export class StayDetailsComponent implements OnInit {
  stay!: AccommodationVM;

  constructor(private _route: ActivatedRoute, private _http: HttpClient) {}

  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      this._http
        .get<AccommodationVM>(
          `https://localhost:44325/api/Accommodation/get/${params.id}`
        )
        .subscribe((data) => {
          this.stay = data;
          console.log('data :>> ', data);
        });
    });
  }
}
