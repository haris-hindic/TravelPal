import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private aRoute: ActivatedRoute, private user: UserService) {}

  ngOnInit(): void {
    this.confirmEmail();
  }

  private confirmEmail = () => {
    this.showError = this.showSuccess = false;

    const token = this.aRoute.snapshot.queryParams['token'];
    const email = this.aRoute.snapshot.queryParams['email'];

    console.log(token);

    this.user
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
