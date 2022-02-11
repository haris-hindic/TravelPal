import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { PaginatedResult } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  url = 'https://localhost:44325/api/message/';

  constructor(private http: HttpClient) {}

  getMessages<T>(pageNumber: number, pageSize: number, container: string, userId: string) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('Container', container);
    params = params.append('userId', userId);

    return this.getResult(params);
  }

  getResult(params: HttpParams) {
    const paginatedResult = new PaginatedResult();

    return this.http.get(this.url, { observe: 'response', params }).pipe(
      map((response: any) => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(
            response.headers.get('Pagination')
          );
        }
        return paginatedResult;
      })
    );
  }

   readMessages(id: string)
   {
     return this.http.get(this.url + `readMessages?id=${id}`);
   }
}
