import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import { Message } from 'src/app/shared/models/message.model';
import { userEditVM } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/user/user.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css'],
})
export class ConversationComponent implements OnInit {
  primaryUserId!: string;
  // @Input() secondUserId!: string;
  secondUserId!: string;
  formData!: FormGroup;
  messages!: any;
  input: string = '';
  recipient: any;

  constructor(
    private securityService: SecurityService,
    private messageService: MessageService,
    private aRoute: ActivatedRoute,
    private builder: FormBuilder,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    
    this.loadData();

    this.formData = this.builder.group({
      senderId: [
        this.securityService.getFieldFromJWT('id'),
        { Validators: [Validators.required] },
      ],
      recipientId: [this.secondUserId, { Validators: [Validators.required] }],
      content: ['', { Validators: [Validators.required] }],
    });

    this.loadMessages();
  }

  loadData()
  {
    this.aRoute.params.subscribe((x) => {
      this.secondUserId = x['id'];
    });
    this.primaryUserId = this.securityService.getFieldFromJWT('id');
    this.userService.getUserById(this.secondUserId).subscribe(x=>
      {
        this.recipient = x;
        console.log(this.recipient);
      })
  }

  loadMessages() {
    this.messageService
      .getConversation(this.primaryUserId, this.secondUserId)
      .subscribe((x: any) => {
        this.messages = x;
        console.log(this.messages);
        this.messageService.readMessages(this.primaryUserId).subscribe();
      });
  }

  sendMessage() {
    console.log(this.formData.value);
    this.messageService.sendMessage(this.formData.value).subscribe((x) => {
      this.loadMessages();
      this.input = '';
    });
  }
}
