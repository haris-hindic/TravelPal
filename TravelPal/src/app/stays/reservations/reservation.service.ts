import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReservationCreationVM, ReservationVM } from './reservation.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiURL = `https://localhost:44325/api/Reservation`;
  private storageKey = 'reservation';

  constructor(private _http: HttpClient) {}

  setReservationInfo(reservation: ReservationCreationVM) {
    const reservationJson = JSON.stringify(reservation);

    localStorage.setItem(this.storageKey, reservationJson);
  }

  getReservationInfo(): ReservationCreationVM {
    const reservation: ReservationCreationVM = JSON.parse(
      localStorage.getItem(this.storageKey) as string
    );
    localStorage.removeItem(this.storageKey);
    return reservation;
  }

  create(reservation: ReservationCreationVM) {
    return this._http.post(`${this.apiURL}/create`, reservation);
  }

  getByUser(id: string) {
    return this._http.get<ReservationVM[]>(
      `${this.apiURL}/user-reservations/${id}`
    );
  }

  getByStay(id: number) {
    return this._http.get<ReservationVM[]>(
      `${this.apiURL}/stay-reservations/${id}`
    );
  }
}
