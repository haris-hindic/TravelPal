import { Component, Input, OnInit } from '@angular/core';
import { Image } from 'src/app/shared/models/image.model';

@Component({
  selector: 'app-image-slider',
  templateUrl: './image-slider.component.html',
  styleUrls: ['./image-slider.component.css'],
})
export class ImageSliderComponent implements OnInit {
  @Input() images!: Array<Image>;
  imageObject: Array<object> = [];
  constructor() {}

  ngOnInit(): void {
    this.images.forEach((img) => {
      this.imageObject.push({
        image: img.imagePath,
        thumbImage: img.imagePath,
      });
    });
  }
}
