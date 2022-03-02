import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { AccommodationService } from 'src/app/stays/accommodation.service';
import { AccommodationBasicVM } from 'src/app/stays/stays.model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-stay-list',
  templateUrl: './stay-list.component.html',
  styleUrls: ['./stay-list.component.css'],
})
export class StayListComponent implements OnInit {
  stays!: AccommodationBasicVM[];
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private _toastr: ToastrService,
    private _admin: AdminService,
    public _security: SecurityService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._admin.getStays(this.pageNumber, this.pageSize).subscribe((res) => {
      this.stays = res.result;
      this.pagination = res.pagination;
    });
  }

  delete(id: number) {
    this._admin.deleteStay(id).subscribe((res) => {
      this.loadData();
      this._toastr.error('Successfully deleted!');
    });
  }

  updatePagination(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }
}
