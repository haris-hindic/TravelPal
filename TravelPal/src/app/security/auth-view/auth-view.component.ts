import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-auth-view',
  templateUrl: './auth-view.component.html',
  styleUrls: ['./auth-view.component.css'],
})
export class AuthViewComponent implements OnInit {
  constructor(private _securityService: SecurityService) {}

  ngOnInit(): void {}

  isAuthorized() {
    return this._securityService.isAuthenticated();
  }
}
