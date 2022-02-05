import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SecurityService } from '../security/security.service';
import { AccommodationService } from './accommodation.service';
import { AccommodationVM } from './stays.model';

@Component({
  selector: 'app-stays',
  templateUrl: './stays.component.html',
  styleUrls: ['./stays.component.css'],
})
export class StaysComponent implements OnInit {
  location!: string;
  constructor(private _router: Router) {}

  ngOnInit(): void {}

  search() {
    console.log(this.location);
    if (!this.location) {
      this._router.navigate([`/stays/search`]);
    } else {
      this._router.navigate([`/stays/search/${this.location}`]);
    }
  }
}
