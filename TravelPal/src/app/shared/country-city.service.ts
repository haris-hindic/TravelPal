import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { city, country } from './models/location.model';

@Injectable({
  providedIn: 'root',
})
export class CountryCityService {
  apiCountryURL = 'https://api.countrystatecity.in/v1/countries';
  constructor(private _http: HttpClient) {}

  getCountries() {
    return this._http.get<country[]>(this.apiCountryURL, {
      headers: new HttpHeaders({
        'X-CSCAPI-KEY':
          'cXB2ZlJ0bDMzbURVZXRtWjBhZmhYRHRNUG5UY3hKRFJ6RGdtV2swSQ==',
      }),
    });
  }

  getCitiesByCountry(iso2: string) {
    return this._http.get<city[]>(
      `https://api.countrystatecity.in/v1/countries/${iso2}/cities`,
      {
        headers: new HttpHeaders({
          'X-CSCAPI-KEY':
            'cXB2ZlJ0bDMzbURVZXRtWjBhZmhYRHRNUG5UY3hKRFJ6RGdtV2swSQ==',
        }),
      }
    );
  }
}
