import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomEncoder } from '../helpers/custom-encode/custom-encode.component.spec';
import {
  authResponse,
  signInCredentials,
  signUpCredentials,
} from './userCredentials.model';

@Injectable({
  providedIn: 'root',
})
export class SecurityService {
  private tokenKey: string = 'token';
  private expirationTokenKey: string = 'token-expiration';
  private roleKey: string = 'role';
  constructor(private _http: HttpClient) {}

  isAuthenticated() {
    const token = localStorage.getItem(this.tokenKey);

    if (!token) return false;

    const expiration = localStorage.getItem(this.expirationTokenKey);
    const expirationDate = new Date(expiration as string);

    if (expirationDate <= new Date()) {
      this.logout();
      return false;
    }

    return true;
  }

  isAdmin() {
    const role = this.getFieldFromJWT(this.roleKey);

    if (role === 'admin') return true;

    return false;
  }

  // public confirmEmailroute( route : string, token: string, email: string)
  // {
  //   let params = new HttpParams({ encoder: new CustomEncoder () })
  //  params = params.append('token', token);
  //   params = params.append('email', email);

  //   return this._http.get('https://localhost:44325/' + route, { params: params });
  // }

  signUp(credentials: signUpCredentials) {
    return this._http.post<authResponse>(
      'https://localhost:44325/api/accounts/signUp',
      credentials
    );
  }

  signIn(credentials: signInCredentials) {
    return this._http.post<authResponse>(
      'https://localhost:44325/api/accounts/signIn',
      credentials
    );
  }

  saveToken(authRes: authResponse) {
    localStorage.setItem(this.tokenKey, authRes.token);
    localStorage.setItem(
      this.expirationTokenKey,
      authRes.expiration.toString()
    );
  }

  getToken() {
    return localStorage.getItem(this.tokenKey);
  }

  getFieldFromJWT(field: string) {
    const token = localStorage.getItem(this.tokenKey);

    if (!token) return '';
    const dataToken = JSON.parse(atob(token.split('.')[1]));
    return dataToken[field];
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.expirationTokenKey);
  }
}
