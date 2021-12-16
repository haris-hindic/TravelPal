import { Component, OnInit } from '@angular/core';
import { userVM } from 'src/app/models/user.model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  users!: userVM[];

  constructor(private _adminService: AdminService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._adminService.getUsers().subscribe((res) => {
      this.users = res;
    });
  }

  makeAdmin(id: string) {
    this._adminService.makeAdmin(id).subscribe(() => this.loadData());
  }

  removeAdmin(id: string) {
    this._adminService.removeAdmin(id).subscribe(() => this.loadData());
  }
}
