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
    return this.http.get(this.url + '/event');
  }
  getSpecific(id: number)
  {
    return this.http.get(this.url+ '/event/' + id);
  }

  post(event: EventCreationVM)
  {
    return this.http.post(this.url + '/event/', event);
  }
  delete(id: number)
  {
    return this.http.delete(this.url + '/event/' + id, {responseType: 'text'});
  }

  edit(id: number, event: EventEditVM)
  {
    return this.http.put(this.url+'/event/' + id, event)
  }
}