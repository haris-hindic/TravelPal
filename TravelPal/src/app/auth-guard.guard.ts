import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { SecurityService } from './security/security.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardGuard implements CanActivate {
  constructor(private _security: SecurityService, private _router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (this._security.isAuthenticated()) {
      return true;
    }

    this._router.navigate(['signin']);
    return false;
  }
}
