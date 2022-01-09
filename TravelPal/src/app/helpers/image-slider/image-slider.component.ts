import { Component, Input, OnInit } from '@angular/core';
import { Image } from 'src/app/shared/models/image.model';

@Component({
  selector: 'app-image-slider',
  templateUrl: './image-slider.component.html',
  styleUrls: ['./image-slider.component.css'],
})
export class ImageSliderComponent implements OnInit {
  @Input() images!: Array<Image>;
  @Input() isEvent!: boolean; 
  size = {};

  imageObject: Array<object> = [];

  constructor() { }



  ngOnInit(): void {
    
    this.getSize();
    this.images.forEach((img) => {
      this.imageObject.push({
        image: img.imagePath,
        thumbImage: img.imagePath,
      });
    });
  }

  getSize()
  {
    if (this.isEvent) {

      this.size = { width: 466, height: 350}
    }
    else
    {
      this.size = {height: 500, width: 800};
    }
    console.log(this.size);
  }
}
