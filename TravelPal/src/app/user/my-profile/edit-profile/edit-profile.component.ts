import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { userEditVM, userProfileVM } from 'src/app/shared/models/user.model';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit {
  form!: FormGroup;
  @Input() user!: userProfileVM;

  @Output() result = new EventEmitter<userEditVM>();
  @Output() closeModal = new EventEmitter<void>();

  constructor(private _formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      userName: ['', { validators: [Validators.required] }],
      firstName: ['', { validators: [Validators.required] }],
      lastName: ['', { validators: [Validators.required] }],
    });

    this.PatchValues();
  }

  PatchValues() {
    this.form.get('userName')?.patchValue(this.user.userName);
    this.form.get('firstName')?.patchValue(this.user.firstName);
    this.form.get('lastName')?.patchValue(this.user.lastName);
  }

  saveChanges() {
    this.result.emit(this.form.value);
  }

  close() {
    this.closeModal.emit();
  }
}
