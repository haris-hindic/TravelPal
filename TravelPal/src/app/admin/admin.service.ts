import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { EventVM } from '../events/events.model';
import { PaginatedResult } from '../shared/models/pagination';
import { userVM } from '../shared/models/user.model';
import { AccommodationBasicVM } from '../stays/stays.model';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiKey: string = `https://localhost:44325/api/Admin`;

  paginatdUsers: PaginatedResult<userVM[]> = new PaginatedResult<userVM[]>();
  paginatdStays: PaginatedResult<AccommodationBasicVM[]> = new PaginatedResult<
    AccommodationBasicVM[]
  >();
  paginatedEvents: PaginatedResult<EventVM[]> = new PaginatedResult<
    EventVM[]
  >();

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
          this.paginatdUsers.result = res.body;
          if (res.headers.get('Pagination') !== null) {
            this.paginatdUsers.pagination = JSON.parse(
              res.headers.get('Pagination')
            );
          }
          return this.paginatdUsers;
        })
      );
  }

  getStays(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this._http
      .get<AccommodationBasicVM[]>(`${this.apiKey}/getStays`, {
        observe: 'response',
        params,
      })
      .pipe(
        map((res: any) => {
          this.paginatdStays.result = res.body;
          if (res.headers.get('Pagination') !== null) {
            this.paginatdStays.pagination = JSON.parse(
              res.headers.get('Pagination')
            );
          }
          return this.paginatdStays;
        })
      );
  }

  deleteStay(id: number) {
    const headers = new HttpHeaders('Content-Type: application/json');
    return this._http.post(`${this.apiKey}/deleteStay`, JSON.stringify(id), {
      headers,
    });
  }

  getEvents(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this._http
      .get<EventVM[]>(this.apiKey + '/getEvents', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response: any) => {
          this.paginatedEvents.result = response.body;
          if (response.headers.get('Pagination') != null) {
            this.paginatedEvents.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return this.paginatedEvents;
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

  deleteEvent(id: number) {
    const headers = new HttpHeaders('Content-Type: application/json');
    return this._http.post(`${this.apiKey}/deleteEvent`, JSON.stringify(id), {
      headers,
    });
  }
}
