import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { EventSignUpCreationVM, EventSignUpVM } from './eventsSignUp.model';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/shared/models/pagination';

@Injectable({
  providedIn: 'root',
})
export class EventsSignUpService {
  url = 'https://localhost:44325/api/eventSignUp/';
  paginatedResult: PaginatedResult<EventSignUpVM[]> = new PaginatedResult<
    EventSignUpVM[]
  >();

  constructor(private http: HttpClient) {}

  getAll(id: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http
      .get<EventSignUpVM[]>(this.url + `getByUserId/${id}`, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response: any) => {
          this.paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            this.paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return this.paginatedResult;
        })
      );
  }

  cancelSignUp(id: number) {
    return this.http.get(this.url + `CancelSignUp/${id}`);
  }

  addSignUp(data: EventSignUpCreationVM) {
    return this.http.post(this.url, data);
  }

  getAllHostedSignUps(
    id: string,
    eventId: number,
    page?: number,
    itemsPerPage?: number
  ) {
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http
      .get<EventSignUpVM[]>(this.url + `getHostedSignUps/${id}/${eventId}`, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response: any) => {
          this.paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            this.paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return this.paginatedResult;
        })
      );
  }
}
