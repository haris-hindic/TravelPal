import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/shared/models/pagination';
import {
  ReservationCreationVM,
  ReservationTempInfo,
  ReservationUserInfoVM,
  ReservationVM,
} from './reservation.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiURL = `https://localhost:44325/api/Reservation`;
  private storageKey = 'reservation';

  paginatdResult: PaginatedResult<ReservationVM[]> = new PaginatedResult<
    ReservationVM[]
  >();

  constructor(private _http: HttpClient) {}

  getUserInfo(id: string) {
    return this._http.get<ReservationUserInfoVM>(
      `${this.apiURL}/user-info/${id}`
    );
  }

  setReservationInfo(reservation: ReservationTempInfo) {
    const reservationJson = JSON.stringify(reservation);

    localStorage.setItem(this.storageKey, reservationJson);
  }

  getReservationInfo(): ReservationTempInfo {
    const reservation: ReservationTempInfo = JSON.parse(
      localStorage.getItem(this.storageKey) as string
    );

    return reservation;
  }

  removeReservationInfo() {
    localStorage.removeItem(this.storageKey);
  }

  create(reservation: ReservationCreationVM) {
    return this._http.post(`${this.apiURL}/create`, reservation);
  }

  getByUser(id: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this._http
      .get<ReservationVM[]>(`${this.apiURL}/user-reservations/${id}`, {
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

  getByHost(id: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this._http
      .get<ReservationVM[]>(`${this.apiURL}/host-reservations/${id}`, {
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

  cancel(id: number) {
    return this._http.get(`${this.apiURL}/cancel/${id}`);
  }

  confirm(id: number) {
    return this._http.get(`${this.apiURL}/confirm/${id}`);
  }
}
