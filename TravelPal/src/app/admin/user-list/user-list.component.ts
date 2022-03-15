import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { Pagination } from 'src/app/shared/models/pagination';
import { userVM } from 'src/app/shared/models/user.model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  users!: userVM[];
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private _adminService: AdminService,
    private _toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._adminService
      .getUsers(this.pageNumber, this.pageSize)
      .subscribe((res) => {
        this.users = res.result;
        this.pagination = res.pagination;
      });
  }

  updatePagination(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  makeAdmin(id: string) {
    this._adminService.makeAdmin(id).subscribe(() => {
      this.loadData();
      this._toastr.info('User successfully made admin');
    });
  }

  removeAdmin(id: string) {
    this._adminService.removeAdmin(id).subscribe(() => {
      this.loadData();
      this._toastr.info('User successfully removed as admin');
    });
  }
}
