import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventCreationVM, EventEditVM, EventVM } from './events.model';
@Injectable({
  providedIn: 'root'
})
export class EventsService {

  url = 'https://localhost:44325/api';
  constructor(private http: HttpClient) {}

  get()
  {
    return this.http.get(this.url + '/Event');
  }
  getSpecific(id: number)
  {
    return this.http.get(this.url+ '/Event/' + id);
  }

  post(event: EventCreationVM)
  {
    return this.http.post(this.url + '/Event/', event);
  }
  delete(id: number)
  {
    return this.http.delete(this.url + '/Event/' + id, {responseType: 'text'});
  }

  edit(id: number, event: EventEditVM)
  {
    return this.http.put(this.url+'/Event/' + id, event)
  }
}