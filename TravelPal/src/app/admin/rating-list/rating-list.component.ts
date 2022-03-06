import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { Pagination } from 'src/app/shared/models/pagination';
import { RatingVM } from 'src/app/shared/models/rating.model';
import { userVM } from 'src/app/shared/models/user.model';
import { AccommodationService } from 'src/app/stays/accommodation.service';
import { AccommodationVM } from 'src/app/stays/stays.model';
import { UserService } from 'src/app/user/user.service';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-rating-list',
  templateUrl: './rating-list.component.html',
  styleUrls: ['./rating-list.component.css']
})
export class RatingListComponent implements OnInit {

  ratings!: RatingVM[];
  user!: userVM;
  stay!: AccommodationVM;

   // pagination
   pagination!: Pagination;
   pageNumber = 1;
   pageSize = 4;

   
  constructor(private adminService: AdminService, private toastr: ToastrService, private acommodationService: AccommodationService,
    private userService: UserService) { }

  ngOnInit(): void {
    this.loadRatings();
  }
  
  loadRatings()
  {
    this.adminService.getRatings(this.pageNumber,this.pageSize).subscribe(x=>
    { 
      this.ratings = x.result;
      this.pagination = x.pagination;
      console.log(this.ratings);
    });
  }


  onChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadRatings();
  }

  deleteRating(id: number)
  {
    this.adminService.deleteRating(id).subscribe(x=>
      {
        this.loadRatings();
        this.toastr.error("Rating deleted!");
      },
      err=>{
        this.toastr.error(err.error);
      })
  }
}
