import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AccommodationCreationVM,
  AccommodationEditVM,
  AccommodationSearchVM,
  AccommodationVM,
} from './stays.model';

@Injectable({
  providedIn: 'root',
})
export class AccommodationService {
  _apiURL = `https://localhost:44325/api/accommodation`;

  constructor(private _http: HttpClient) {}

  getAll(search: AccommodationSearchVM) {
    return this._http.get<AccommodationVM[]>(
      `${this._apiURL}?location=${
        !search.location ? '' : search.location
      }&price=${!search.price ? 0 : search.price}`
    );
  }

  ownerShip(userId: string, accommodationId: number) {
    return this._http.get<boolean>(
      `${this._apiURL}/ownership?userId=${userId}&accommodationId=${accommodationId}`
    );
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
