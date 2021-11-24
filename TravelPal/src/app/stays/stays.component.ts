import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccommodationVM } from './stays.model';

@Component({
  selector: 'app-stays',
  templateUrl: './stays.component.html',
  styleUrls: ['./stays.component.css'],
})
export class StaysComponent implements OnInit {
  stays!: AccommodationVM[];

  constructor(private _http: HttpClient) {}

  ngOnInit(): void {
    this._http
      .get<AccommodationVM[]>(
        'https://localhost:44325/api/Accommodation/getall'
      )
      .subscribe((data) => {
        console.log(data);
        this.stays = data;
      });
  }
}
