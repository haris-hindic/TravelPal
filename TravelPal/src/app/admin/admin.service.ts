import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../shared/models/pagination';
import { userVM } from '../shared/models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiKey: string = `https://localhost:44325/api/Admin`;

  paginatdResult: PaginatedResult<userVM[]> = new PaginatedResult<userVM[]>();

  constructor(private _http: HttpClient) {}

  getUsers(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this._http
      .get<userVM[]>(`${this.apiKey}/users`, {
        observe: 'response',
        params,
      })
      .pipe(
        map((res: any) => {
          this.paginatdResult.result = res.body;
          if (res.headers.get('Pagination') !== null) {
            this.paginatdResult.pagination = JSON.parse(
              res.headers.get('Pagination')
            );
          }
          return this.paginatdResult;
        })
      );
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

  deleteUser(id: string) {
    const headers = new HttpHeaders('Content-Type: application/json');
    return this._http.post(`${this.apiKey}/deleteUser`, JSON.stringify(id), {
      headers,
    });
  }
}
