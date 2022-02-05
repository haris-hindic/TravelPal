import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { SecurityService } from 'src/app/security/security.service';
import { AccommodationService } from '../accommodation.service';
import { AccommodationSearchVM, AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-search',
  templateUrl: './stay-search.component.html',
  styleUrls: ['./stay-search.component.css'],
})
export class StayHomepageComponent implements OnInit {
  stays!: AccommodationVM[];
  searchForm!: FormGroup;

  constructor(
    private _accommodationService: AccommodationService,
    public _securityService: SecurityService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      this.searchForm = this._formBuilder.group({
        location: '',
        price: 0,
      });

      this.searchForm.get('location')!.patchValue(params.location);
      this.loadData(this.searchForm.value);
    });
  }

  searchStays() {
    this.loadData(this.searchForm.value);
  }

  loadData(search: AccommodationSearchVM) {
    this._accommodationService.getAll(search).subscribe((data) => {
      this.stays = data;
    });
  }
}
