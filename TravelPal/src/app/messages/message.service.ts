import { HttpClient, HttpParams } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { take } from 'rxjs/operators';
import { SecurityService } from '../security/security.service';
import { Message } from '../shared/models/message.model';
import { PaginatedResult } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  url = 'https://localhost:44325/api/message/';
  hubUrl = 'https://localhost:44325/hubs/';
  private hubConnect!: HubConnection;
  private msgConversationSource = new BehaviorSubject<Message[]>([]);


  msgConversation$ = this.msgConversationSource.asObservable();

  rMessages = new EventEmitter<boolean>();

  constructor(
    private http: HttpClient,
    private securityService: SecurityService
  ) {

  }

  getMessages<T>(
    pageNumber: number,
    pageSize: number,
    container: string,
    userId: string
  ) {
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

  async sendMessage(data: any) {
    return this.hubConnect
      .invoke('SendMessage', data)
      .catch((err) => console.log(err));
  }

  readMessages(id: string) {
    return this.http.get(this.url + `readMessages?id=${id}`);
  }

  getConversation(userOne: string, userTwo: string) {
    return this.http.get(this.url + 'conversation/' + userOne + '/' + userTwo);
  }

  deleteMessage(msgId: number, userId: string) {
    return this.http.delete(this.url + `${userId}/${msgId}`);
  }

  createHubConnection(currentUserId: string, otherUserId: string) {
    this.hubConnect = new HubConnectionBuilder()
      .withUrl(
        this.hubUrl + `message/?user=${currentUserId}&other=${otherUserId}`,
        {
          accessTokenFactory: () => this.securityService.getToken()!,
        }
      )
      .withAutomaticReconnect()
      .build();

    this.hubConnect.start().catch((err) => console.log(err));

    this.hubConnect.on('ReceiveMessageConversation', (messages) => {
      this.msgConversationSource.next(messages);
    });

    this.hubConnect.on('NewMessage', (message) => {
      this.msgConversation$.pipe(take(1)).subscribe((x) => {
        this.msgConversationSource.next([...x, message]);
      });
    });


  }

  stopHubConnection() {
    this.hubConnect.stop();
  }
}
