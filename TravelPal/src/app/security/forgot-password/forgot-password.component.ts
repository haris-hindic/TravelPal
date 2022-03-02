import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { parseWebAPiErrors } from 'src/app/helpers/parseWebAPIErrors';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  @Output() closeModal = new EventEmitter<void>();
  errors:string[] = [];
  email!: string;

  constructor(private securityService: SecurityService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  close()
  {
    this.closeModal.emit();
  }

  saveChanges()
  {
    this.securityService.forgotPassword(this.email).subscribe(x=>
      {
        this.toastr.success("Email with token for reset password was sent!");
        this.close();
      },
      err=>
      {
        this.errors = parseWebAPiErrors(err);
      });
  }

}
