import { Component, Input, OnInit } from '@angular/core';
import { RatingVM } from '../models/rating.model';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css'],
})
export class RatingComponent implements OnInit {
  @Input() rating!: RatingVM;
  stars: number[] = [];

  constructor() {}

  ngOnInit(): void {
    for (let i = 0; i < this.rating.rate; i++) {
      this.stars.push(i);
    }
  }
}
