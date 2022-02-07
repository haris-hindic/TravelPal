import { TOUCH_BUFFER_MS } from '@angular/cdk/a11y/input-modality/input-modality-detector';
import {
  HttpClient,
  HttpHeaderResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { EventVM } from 'src/app/events/events.model';
import { EventsService } from 'src/app/events/events.service';
import { SecurityService } from 'src/app/security/security.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { userEditVM, userProfileVM } from 'src/app/shared/models/user.model';
import { AccommodationService } from 'src/app/stays/accommodation.service';
import { AccommodationVM } from 'src/app/stays/stays.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent implements OnInit {
  user!: userProfileVM;
  userStays: AccommodationVM[] = [];
  userEvents!: EventVM[];

  showEditProfile = false;
  showVerify = false;
  showPicture = false;

  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 4;

  constructor(
    public _security: SecurityService,
    private _user: UserService,
    private _accommodation: AccommodationService,
    private _toastr: ToastrService,
    private _eventService: EventsService
  ) {}

  ngOnInit(): void {
    this.LoadData();
  }

  LoadData() {
    const id = this._security.getFieldFromJWT('id');

    this._user.getUserById(id).subscribe((res) => {
      this.user = res;
    });
    if (this._security.isVerified()) {
      this._accommodation
        .getByUser(id, this.pageNumber, this.pageSize)
        .subscribe((res) => {
          this.userStays = res.result;
          this.pagination = res.pagination;
        });
    }

    this._eventService.getUserEvents(id).subscribe((res) => {
      this.userEvents = res;
      console.log(this.userEvents);
    });
  }

  updatePagination(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.LoadData();
  }

  editProfile() {
    this.showEditProfile = true;
  }

  verifyProfile() {
    this.showVerify = true;
  }

  onEditProfile(editProfile: userEditVM) {
    this._user
      .updateProfile(this._security.getFieldFromJWT('id'), editProfile)
      .subscribe(() => {
        this.showEditProfile = false;
        this._toastr.success('Successfully edited!');
        this.LoadData();
      });
  }

  profilePicture() {
    this.showPicture = true;
  }
}
