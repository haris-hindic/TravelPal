import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from 'src/app/security/security.service';
import { UserService } from 'src/app/user/user.service';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css'],
})
export class EmailConfirmationComponent implements OnInit {
  public showSuccess!: boolean;
  public showError!: boolean;
  public errorMessage!: string;

  constructor(private _route: ActivatedRoute, private _user: UserService) {}

  ngOnInit(): void {
    this.confirmEmail();
  }

  private confirmEmail = () => {
    this.showError = this.showSuccess = false;

    const token = this._route.snapshot.queryParams['token'];
    const email = this._route.snapshot.queryParams['email'];

    console.log(token);

    this._user
      .confirmEmailroute('api/Accounts/emailconfirmation', token, email)
      .subscribe(
        (_) => {
          this.showSuccess = true;
        },
        (error: string) => {
          this.showError = true;
          this.errorMessage = error;
        }
      );
  };
}
