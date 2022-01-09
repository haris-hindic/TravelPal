import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { city, country } from './models/location.model';

@Injectable({
  providedIn: 'root',
})
export class CountryCityService {
  apiURL = `https://localhost:44325/api/Location`;
  constructor(private _http: HttpClient) {}

  getCountries() {
    return this._http.get<country[]>(
      `https://localhost:44325/api/Location/countries`
    );
  }

  getCitiesByCountry(id: number) {
    return this._http.get<city[]>(`${this.apiURL}/cities/${id}`);
  }
}
