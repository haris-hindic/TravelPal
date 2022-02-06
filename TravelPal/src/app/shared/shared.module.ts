import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { MatStepperModule } from '@angular/material/stepper';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgImageSliderModule } from 'ng-image-slider';
import { ToastrModule } from 'ngx-toastr';
import { MatMenuModule } from '@angular/material/menu';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';

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
    MatMenuModule,
    MatTabsModule,
    MatPaginatorModule,
  ],
  exports: [
    SweetAlert2Module,
    ToastrModule,
    AgmCoreModule,
    AgmSnazzyInfoWindowModule,
    MatStepperModule,
    NgImageSliderModule,
    MatMenuModule,
    MatTabsModule,
    MatPaginatorModule,
  ],
})
export class SharedModule {}
