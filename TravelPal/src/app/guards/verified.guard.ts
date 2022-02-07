import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { iif, Observable } from 'rxjs';
import { SecurityService } from '../security/security.service';

@Injectable({
  providedIn: 'root',
})
export class VerifiedGuard implements CanActivate {
  constructor(
    private _securityService: SecurityService,
    private _router: Router,
    private _toastr: ToastrService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    if (this._securityService.isVerified()) {
      return true;
    }

    this._router.navigate(['/user/profile']);
    this._toastr.info(
      'In order to host or reserve you need to verify your profile.'
    );
    return false;
  }
}
