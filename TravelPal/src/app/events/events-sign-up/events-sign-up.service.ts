import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {EventSignUpCreationVM, EventSignUpVM } from './eventsSignUp.model';

@Injectable({
  providedIn: 'root'
})
export class EventsSignUpService {

  url = 'https://localhost:44325/api/eventSignUp/';


  constructor(private http: HttpClient) { }

  getAll(id: string)
  {
    return this.http.get<EventSignUpVM[]>(this.url + `getByUserId/${id}`);
  }

  cancelSignUp(id:number)
  {
    return this.http.get(this.url + `CancelSignUp/${id}`);
  }

  addSignUp(data: EventSignUpCreationVM)
  {
    return this.http.post(this.url, data);
  }
}
