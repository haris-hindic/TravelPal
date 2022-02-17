import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from '../messages/message.service';
import { SecurityService } from '../security/security.service';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  id!: string;
  user!: any;
  flag = false;

  constructor(
    public _securityService: SecurityService,
    private _router: Router,
    private _userService: UserService,
    public _messagesService: MessageService
  ) {}

  ngOnInit(): void {
    this.id = this._securityService.getFieldFromJWT('id');

    this._userService.getUserById(this.id).subscribe((x: any) => {
      for (let index = 0; index < x.messagesReceived.length; index++) {
        if (x.messagesReceived[index].dateRead == null) {
          this.flag = true;
        }
      }

      this._messagesService.rMessages.subscribe((x) => {
        this.flag = false;
      });
    });
  }

  logout() {
    this._securityService.logout();
    this._router.navigate(['']);
  }

  isAdmin() {
    return this._securityService.isAdmin();
  }
}
