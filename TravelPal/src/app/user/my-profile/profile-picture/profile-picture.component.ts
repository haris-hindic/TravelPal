import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { toBase64 } from 'src/app/helpers/toBase64';
import { SecurityService } from 'src/app/security/security.service';
import { userProfileVM, userEditVM } from 'src/app/shared/models/user.model';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.css'],
})
export class ProfilePictureComponent implements OnInit {
  userId!: string;

  image!: File;
  imageBase64!: string;

  @Output() closeModal = new EventEmitter<void>();
  formData: FormData = new FormData();
  constructor(private _security: SecurityService, private _user: UserService) {}

  ngOnInit(): void {
    this.userId = this._security.getFieldFromJWT('id');
  }

  close() {
    this.closeModal.emit();
  }

  imageSelected(event: any) {
    if (event.target.files.length > 0) {
      //this.formData.append('images', event.target.files[0]);
      this.image = event.target.files[0];
      toBase64(event.target.files[0]).then((img) => {
        this.imageBase64 = img;
      });
    }
  }

  submit() {
    this.formData.append('picture', this.image);
    this._user.updatePicture(this.userId, this.formData).subscribe(() => {
      this.close();
    });
  }
}
