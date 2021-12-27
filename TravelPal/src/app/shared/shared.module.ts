import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { MatStepperModule } from '@angular/material/stepper';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgImageSliderModule } from 'ng-image-slider';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SweetAlert2Module.forRoot(),
    ToastrModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAhH0kpOB9F5lpdzbVhi4c5mJyxIBtd43Q',
    }),
    AgmSnazzyInfoWindowModule,
    MatStepperModule,
    NgImageSliderModule,
  ],
  exports: [
    SweetAlert2Module,
    ToastrModule,
    AgmCoreModule,
    AgmSnazzyInfoWindowModule,
    MatStepperModule,
    NgImageSliderModule,
  ],
})
export class SharedModule {}
