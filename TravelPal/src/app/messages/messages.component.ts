import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security/security.service';
import { Pagination } from '../shared/models/pagination';
import { MessageService } from './message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
})
export class MessagesComponent implements OnInit {
  container = 'Outbox';
  messages!: any;
  pageNumber = 1;
  pageSize = 5;
  pagination!: Pagination;
  userId!: string;

  constructor(
    private messageService: MessageService,
    private securityService: SecurityService
  ) {}

  ngOnInit(): void {
    this.userId = this.securityService.getFieldFromJWT('id');
    this.loadData();
  }

  loadData() {
    console.log(this.userId);

    this.messageService
      .getMessages(this.pageNumber, this.pageSize, this.container, this.userId)
      .subscribe((x) => {
        this.messages = x.result;
        this.pagination = x.pagination;
        if (this.container == 'Unread') {
          this.messageService.readMessages(this.userId).subscribe();
        }
      });
  }

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.loadData();
    }
  }

  deleteMessage(id: number)
  {
    this.messageService.deleteMessage(id, this.userId).subscribe(x=>
      {
        this.loadData();
      })
  }
}
