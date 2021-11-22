import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-stays',
  templateUrl: './stays.component.html',
  styleUrls: ['./stays.component.css'],
})
export class StaysComponent implements OnInit {
  stays!: any[];

  constructor(private _http: HttpClient) {}

  ngOnInit(): void {
    this._http
      .get('https://localhost:44325/api/Accommodation')
      .subscribe((data) => {
        console.log(data);
        this.stays = data as [];
      });
  }
}
