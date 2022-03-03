import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SecurityService } from 'src/app/security/security.service';
import { RatingCreationVM } from '../models/rating.model';

@Component({
  selector: 'app-submit-rating',
  templateUrl: './submit-rating.component.html',
  styleUrls: ['./submit-rating.component.css'],
})
export class SubmitRatingComponent implements OnInit {
  @Output() closeModal = new EventEmitter<void>();
  @Output() result = new EventEmitter<RatingCreationVM>();
  @Input() stayID!: number;
  form!: FormGroup;
  constructor(private _fb: FormBuilder, private _security: SecurityService) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      accommodationId: this.stayID,
      userId: this._security.getFieldFromJWT('id'),
      rate: 0,
      comment: '',
    });
  }

  close() {
    this.closeModal.emit();
  }

  submit() {
    console.log(this.form.value);
    this.result.emit(this.form.value);
  }
}
