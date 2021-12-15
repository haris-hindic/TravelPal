import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AccommodationCreationVM,
  AccommodationEditVM,
  AccommodationVM,
} from './stays.model';

@Injectable({
  providedIn: 'root',
})
export class AccommodationService {
  _apiURL = `https://localhost:44325/api/accommodation`;

  constructor(private _http: HttpClient) {}

  getAll() {
    return this._http.get<AccommodationVM[]>(this._apiURL);
  }

  getById(id: number) {
    return this._http.get<AccommodationVM>(`${this._apiURL}/${id}`);
  }

  getByUser(id: string) {
    return this._http.get<AccommodationVM[]>(`${this._apiURL}/user/${id}`);
  }

  add(accommodation: AccommodationCreationVM) {
    return this._http.post(this._apiURL, accommodation);
  }

  update(id: number, accommodation: AccommodationEditVM) {
    return this._http.put(`${this._apiURL}/${id}`, accommodation);
  }

  delete(id: number) {
    return this._http.delete(`${this._apiURL}/${id}`);
  }
}
