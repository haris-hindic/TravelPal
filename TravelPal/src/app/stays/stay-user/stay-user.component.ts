import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccommodationService } from '../accommodation.service';
import { AccommodationVM } from '../stays.model';

@Component({
  selector: 'app-stay-user',
  templateUrl: './stay-user.component.html',
  styleUrls: ['./stay-user.component.css'],
})
export class StayUserComponent implements OnInit {
  stays!: AccommodationVM[];

  constructor(
    private _accommodationService: AccommodationService,
    private _route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this._accommodationService
      .getByUser(this._route.snapshot.params.id)
      .subscribe((res) => {
        this.stays = res;
      });
  }

  delete(id: number) {
    this._accommodationService.delete(id).subscribe((res) => this.loadData());
  }
}
