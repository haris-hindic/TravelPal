import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SecurityService } from '../security/security.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {

  id!: string;

  constructor(
    public _securityService: SecurityService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.id = this._securityService.getFieldFromJWT('id');
  }

  logout() {
    this._securityService.logout();
    this._router.navigate(['']);
  }

  isAdmin() {
    return this._securityService.isAdmin();
  }
}
