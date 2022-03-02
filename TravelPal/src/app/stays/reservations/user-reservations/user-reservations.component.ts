import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { AccommodationService } from '../../accommodation.service';
import { ReservationVM } from '../reservation.model';
import { ReservationService } from '../reservation.service';

@Component({
  selector: 'app-user-reservations',
  templateUrl: './user-reservations.component.html',
  styleUrls: ['./user-reservations.component.css'],
})
export class UserReservationsComponent implements OnInit {
  reservations!: ReservationVM[];
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private _reservation: ReservationService,
    private _toastr: ToastrService,
    public _securityService: SecurityService
  ) {}
  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._reservation
      .getByUser(
        this._securityService.getFieldFromJWT('id'),
        this.pageNumber,
        this.pageSize
      )
      .subscribe((x) => {
        console.log(x);
        this.reservations = x.result;
        this.pagination = x.pagination;
      });
  }

  updatePagination(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  cancel(id: number) {
    this._reservation.cancel(id).subscribe(() => {
      this.loadData();
      this._toastr.info('Successfully cancelled your reservation!');
    });
  }
}
