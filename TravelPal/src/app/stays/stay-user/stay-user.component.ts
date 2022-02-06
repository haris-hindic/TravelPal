import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { Pagination } from 'src/app/shared/models/pagination';
import { AccommodationService } from '../accommodation.service';
import { AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-user',
  templateUrl: './stay-user.component.html',
  styleUrls: ['./stay-user.component.css'],
})
export class StayUserComponent implements OnInit {
  stays!: AccommodationVM[];
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private _accommodationService: AccommodationService,
    private _route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._accommodationService
      .getByUser(this._route.snapshot.params.id, this.pageNumber, this.pageSize)
      .subscribe((res) => {
        this.stays = res.result;
        this.pagination = res.pagination;
      });
  }

  delete(id: number) {
    this._accommodationService.delete(id).subscribe((res) => this.loadData());
  }

  updatePagination(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }
}
