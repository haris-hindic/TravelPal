import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { PaginatedResult } from '../shared/models/pagination';
import {
  EventCreationVM,
  EventEditVM,
  EventSearchVM,
  EventVM,
} from './events.model';
@Injectable({
  providedIn: 'root',
})
export class EventsService {
  url = 'https://localhost:44325/api/event';
  paginatedResult: PaginatedResult<EventVM[]> = new PaginatedResult<EventVM[]>();

  constructor(private http: HttpClient) {}

  get(page?: number, itemsPerPage?: number ) {
    let params = new HttpParams();
    if(page!=null && itemsPerPage !=null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<EventVM[]>(this.url, {observe: 'response', params}).pipe(map((response:any)=>
      {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination')!=null)
        {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      }));

  }
  getSpecific(id: number) {
    return this.http.get<EventVM>(this.url + '/' + id);
  }

  post(event: EventCreationVM) {
    return this.http.post(this.url, event);
  }
  delete(id: number) {
    return this.http.delete(this.url + '/' + id, { responseType: 'text' });
  }

  edit(id: number, event: EventEditVM) {
    return this.http.put(this.url + '/' + id, event);
  }

  getUserEvents(id: string, page?: number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page!=null && itemsPerPage !=null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<EventVM[]>(this.url + '/user/' + id, {observe: 'response', params}).pipe(map((response:any)=>
    {
      console.log(response.headers);
      this.paginatedResult.result = response.body;
      if(response.headers?.get('Pagination')!=null)
      {
        this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return this.paginatedResult;
    }));
  }

  search(eventSearch: EventSearchVM, page?: number, itemsPerPage?: number) {

    let params = new HttpParams();
    if(page!=null && itemsPerPage !=null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get(this.url + `/search?location=${eventSearch.location}&dateFrom=${eventSearch.from}&dateTo=${eventSearch.to}`, {observe: 'response', params})
    .pipe(map((response:any)=>
    {
      console.log(response.headers);
      this.paginatedResult.result = response.body;
      if(response.headers.get('Pagination')!=null)
      {
        this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return this.paginatedResult;
    }));

  }
}

/*import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { PaginatedResult } from '../shared/models/pagination';
import {
  EventCreationVM,
  EventEditVM,
  EventSearchVM,
  EventVM,
} from './events.model';
@Injectable({
  providedIn: 'root',
})
export class EventsService {
  url = 'https://localhost:44325/api/event';
  paginatedResult: PaginatedResult<EventVM[]> = new PaginatedResult<EventVM[]>();

  constructor(private http: HttpClient) {}

  get(page?: number, itemsPerPage?: number ) {
    let params = new HttpParams();
    if(page!=null && itemsPerPage !=null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<EventVM[]>(this.url, {observe: 'response', params}).pipe(map((response:any)=>
      {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination')!=null)
        {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      }));


  }
  getSpecific(id: number) {
    return this.http.get<EventVM>(this.url + '/' + id);
  }

  post(event: EventCreationVM) {
    return this.http.post(this.url, event);
  }
  delete(id: number) {
    return this.http.delete(this.url + '/' + id, { responseType: 'text' });
  }

  edit(id: number, event: EventEditVM) {
    return this.http.put(this.url + '/' + id, event);
  }

  getUserEvents(id: string) {
    return this.http.get<EventVM[]>(this.url + '/user/' + id);
  }

  search(eventSearch: EventSearchVM, page?: number, itemsPerPage?: number ) {

    let params = new HttpParams();
    if(page!=null && itemsPerPage !=null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.post(this.url + '/search', eventSearch, {observe: 'response', params}).pipe(map((response:any)=>
    {
      this.paginatedResult.result = response.body;
      if(response.headers.get('Pagination')!=null)
      {
        this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return this.paginatedResult;
    }));

  }
}
*/