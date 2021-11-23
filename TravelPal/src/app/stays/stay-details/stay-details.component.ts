import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Accommodation } from '../stays.model';

@Component({
  selector: 'app-stay-details',
  templateUrl: './stay-details.component.html',
  styleUrls: ['./stay-details.component.css'],
})
export class StayDetailsComponent implements OnInit {
  stay!: Accommodation;

  constructor(private _route: ActivatedRoute, private _http: HttpClient) {}

  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      console.log('params.id :>> ', params.id);
      this._http
        .get<Accommodation>(
          `https://localhost:44325/api/Accommodation/get/${params.id}`
        )
        .subscribe((data) => {
          this.stay = data;
          console.log('data :>> ', data);
        });
    });
  }
}
