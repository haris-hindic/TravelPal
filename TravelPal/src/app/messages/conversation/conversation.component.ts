import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import { UserService } from 'src/app/user/user.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css'],
})
export class ConversationComponent implements OnInit {
  // @Input() secondUserId!: string;
  secondUserId!: string;
  primaryUserId!: string;
  formData!: FormGroup;
  messages!: any;
  input: string = '';
  recipient: any;
  currentUser: any;

  constructor(
    private securityService: SecurityService,
    public messageService: MessageService,
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

    this.primaryUserId = this.securityService.getFieldFromJWT('id');
    this.messageService.createHubConnection(
      this.primaryUserId,
      this.secondUserId
    );

    // this.loadMessages();

    console.log('hubconnect');
  }

  loadData() {
    this.aRoute.params.subscribe((x) => {
      this.secondUserId = x['id'];
    });
    this.userService.getUserById(this.secondUserId).subscribe((x) => {
      this.recipient = x;
    });
  }

  /*loadMessages() {
    this.messageService
      .getConversation(this.primaryUserId, this.secondUserId)
      .subscribe((x: any) => {
        this.messages = x;
        console.log(this.messages);
        this.messageService.readMessages(this.primaryUserId).subscribe();
      });

  }*/

  sendMessage() {
    this.messageService.sendMessage(this.formData.value).then(() => {
      this.input = '';
    });
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
