import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RatingCreationVM } from './models/rating.model';

@Injectable({
  providedIn: 'root',
})
export class RatingService {
  private apiUrl = `https://localhost:44325/api/Ratings`;
  constructor(private _http: HttpClient) {}

  rate(rating: RatingCreationVM) {
    return this._http.post(`${this.apiUrl}/rate`, rating);
  }
}
