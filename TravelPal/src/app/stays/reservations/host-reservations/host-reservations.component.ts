import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { ReservationVM } from '../reservation.model';
import { ReservationService } from '../reservation.service';

@Component({
  selector: 'app-host-reservations',
  templateUrl: './host-reservations.component.html',
  styleUrls: ['./host-reservations.component.css'],
})
export class HostReservationsComponent implements OnInit {
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
      .getByHost(
        this._securityService.getFieldFromJWT('id'),
        this.pageNumber,
        this.pageSize
      )
      .subscribe((x) => {
        this.reservations = x.result;
        this.pagination = x.pagination;
      });
  }

  updatePagination(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  confirm(id: number) {
    this._reservation.confirm(id).subscribe(() => {
      this.loadData();
      this._toastr.info('Successfully confirmed the reservation!');
    });
  }
}
