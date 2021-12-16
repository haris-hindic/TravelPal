import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { userVM } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiKey: string = `https://localhost:44325/api/accounts`;

  constructor(private _http: HttpClient) {}

  getUsers(): Observable<userVM[]> {
    return this._http.get<userVM[]>(`${this.apiKey}/users`);
  }

  makeAdmin(id: string) {
    const headers = new HttpHeaders('Content-Type: application/json');
    return this._http.post(`${this.apiKey}/makeAdmin`, JSON.stringify(id), {
      headers,
    });
  }

  removeAdmin(id: string) {
    const headers = new HttpHeaders('Content-Type: application/json');
    return this._http.post(`${this.apiKey}/removeAdmin`, JSON.stringify(id), {
      headers,
    });
  }
}
