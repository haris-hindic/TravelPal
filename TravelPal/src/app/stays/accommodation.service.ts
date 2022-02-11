import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedResult } from '../shared/models/pagination';
import {
  AccommodationBasicVM,
  AccommodationCreationVM,
  AccommodationEditVM,
  AccommodationSearchVM,
  AccommodationVM,
} from './stays.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AccommodationService {
  _apiURL = `https://localhost:44325/api/accommodation`;

  paginatdResult: PaginatedResult<AccommodationBasicVM[]> = new PaginatedResult<
    AccommodationBasicVM[]
  >();

  constructor(private _http: HttpClient) {}

  getAll(search: AccommodationSearchVM, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this._http
      .get<AccommodationBasicVM[]>(
        `${this._apiURL}?location=${
          !search.location ? '' : search.location
        }&price=${!search.price ? 0 : search.price}`,
        { observe: 'response', params }
      )
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

  ownerShip(userId: string, accommodationId: number) {
    return this._http.get<boolean>(
      `${this._apiURL}/ownership?userId=${userId}&accommodationId=${accommodationId}`
    );
  }

  getById(id: number) {
    return this._http.get<AccommodationVM>(`${this._apiURL}/${id}`);
  }

  getByUser(id: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== undefined && itemsPerPage !== undefined) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this._http
      .get<AccommodationBasicVM[]>(`${this._apiURL}/user/${id}`, {
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
