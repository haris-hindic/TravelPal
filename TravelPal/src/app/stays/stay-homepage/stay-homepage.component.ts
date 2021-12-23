import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-stay-homepage',
  templateUrl: './stay-homepage.component.html',
  styleUrls: ['./stay-homepage.component.css'],
})
export class StayHomepageComponent implements OnInit {
  constructor(private _router: Router) {}

  ngOnInit(): void {}

  search() {
    this._router.navigate(['stays/search']);
  }
}
