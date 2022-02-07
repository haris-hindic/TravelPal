import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomEncoder } from '../helpers/custom-encode/custom-encode.component.spec';
import { userEditVM, userProfileVM } from '../shared/models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly apiURL = `https://localhost:44325/api/Accounts`;
  constructor(private _http: HttpClient) {}

  getUserById(id: string) {
    return this._http.get<userProfileVM>(`${this.apiURL}/profile?id=${id}`);
  }

  sendEmailVerification(email: string) {
    return this._http.get<userProfileVM>(
      `${this.apiURL}/send-email?email=${email}`
    );
  }

  sendPhoneVerification(id: string) {
    return this._http.get<userProfileVM>(
      `${this.apiURL}/phone-verification?id=${id}`
    );
  }

  checkPhoneVerification(id: string, code: string) {
    return this._http.get<userProfileVM>(
      `${this.apiURL}/check-phone-verification?id=${id}&code=${code}`
    );
  }

  confirmEmailroute(route: string, token: string, email: string) {
    let params = new HttpParams({ encoder: new CustomEncoder() });
    params = params.append('token', token);
    params = params.append('email', email);

    return this._http.get('https://localhost:44325/' + route, {
      params: params,
    });
  }

  updateProfile(id: string, editVM: userEditVM) {
    return this._http.put(`${this.apiURL}/edit-profile/${id}`, editVM);
  }

  updatePicture(id: string, picture: FormData) {
    return this._http.post(`${this.apiURL}/change-photo/${id}`, picture);
  }
}
