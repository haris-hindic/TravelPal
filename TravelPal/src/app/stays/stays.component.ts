import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security/security.service';
import { AccommodationService } from './accommodation.service';
import { AccommodationVM } from './stays.model';

@Component({
  selector: 'app-stays',
  templateUrl: './stays.component.html',
  styleUrls: ['./stays.component.css'],
})
export class StaysComponent implements OnInit {
  stays!: AccommodationVM[];

  constructor(
    private _accommodationService: AccommodationService,
    public _securityService: SecurityService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._accommodationService.getAll().subscribe((data) => {
      this.stays = data;
    });
  }

  delete(id: number) {
    this._accommodationService.delete(id).subscribe((res) => this.loadData());
  }
}
