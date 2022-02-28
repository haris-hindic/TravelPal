import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  getByUser(id: string) {
    return this._http.get<ReservationVM[]>(
      `${this.apiURL}/user-reservations/${id}`
    );
  }

  getByHost(id: string) {
    return this._http.get<ReservationVM[]>(
      `${this.apiURL}/host-reservations/${id}`
    );
  }

  getByStay(id: number) {
    return this._http.get<ReservationVM[]>(
      `${this.apiURL}/stay-reservations/${id}`
    );
  }

  cancel(id: number) {
    return this._http.get(`${this.apiURL}/cancel/${id}`);
  }

  confirm(id: number) {
    return this._http.get(`${this.apiURL}/confirm/${id}`);
  }
}
