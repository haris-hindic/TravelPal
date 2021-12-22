import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { StaysComponent } from './stays/stays.component';
import { EventsComponent } from './events/events.component';
import { AppRoutingModule } from './app-routing.module';
import { StayDetailsComponent } from './stays/stay-details/stay-details.component';
import { EventsDetailsComponent } from './events/events-details/events-details.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { StayCreateComponent } from './stays/stay-create/stay-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StayEditComponent } from './stays/stay-edit/stay-edit.component';
import { EventsCreationComponent } from './events/events-creation/events-creation.component';
import { EventsEditComponent } from './events/events-edit/events-edit.component';
import { DisplayErrorsComponent } from './helpers/display-errors/display-errors.component';
import { AuthViewComponent } from './security/auth-view/auth-view.component';
import { SignUpComponent } from './security/sign-up/sign-up.component';
import { SignInComponent } from './security/sign-in/sign-in.component';
import { JwtInterceptorService } from './security/jwt-interceptor.service';
import { StayUserComponent } from './stays/stay-user/stay-user.component';
import { UserListComponent } from './admin/user-list/user-list.component';
import { ToastrModule } from 'ngx-toastr';
import {
  BrowserAnimationsModule,
  NoopAnimationsModule,
} from '@angular/platform-browser/animations';
import { MapComponent } from './helpers/map/map.component';
import { AgmCoreModule } from '@agm/core';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { MapMultipleMarkersComponent } from './helpers/map-multiple-markers/map-multiple-markers.component';
import { MatStepperModule } from '@angular/material/stepper';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    StaysComponent,
    EventsComponent,
    StayDetailsComponent,
    EventsDetailsComponent,
    StayCreateComponent,
    StayEditComponent,
    EventsCreationComponent,
    EventsEditComponent,
    DisplayErrorsComponent,
    AuthViewComponent,
    SignUpComponent,
    SignInComponent,
    StayUserComponent,
    UserListComponent,
    MapComponent,
    MapMultipleMarkersComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    SweetAlert2Module.forRoot(),
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAhH0kpOB9F5lpdzbVhi4c5mJyxIBtd43Q',
    }),
    AgmSnazzyInfoWindowModule,
    MatStepperModule,
    NoopAnimationsModule,
    FormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
