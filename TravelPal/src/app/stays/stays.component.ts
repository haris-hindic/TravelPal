import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccommodationService } from './accommodation.service';
import { AccommodationVM } from './stays.model';

@Component({
  selector: 'app-stays',
  templateUrl: './stays.component.html',
  styleUrls: ['./stays.component.css'],
})
export class StaysComponent implements OnInit {
  stays!: AccommodationVM[];

  constructor(private _accommodationService: AccommodationService) {}

  ngOnInit(): void {
    this._accommodationService.getAll().subscribe((data) => {
      console.log(data);
      this.stays = data;
    });
  }
}
