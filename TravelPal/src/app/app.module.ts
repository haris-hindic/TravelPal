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
import {
  BrowserAnimationsModule,
  NoopAnimationsModule,
} from '@angular/platform-browser/animations';
import { MapComponent } from './helpers/map/map.component';
import { MapMultipleMarkersComponent } from './helpers/map-multiple-markers/map-multiple-markers.component';
import { StayHomepageComponent } from './stays/stay-homepage/stay-homepage.component';
import { EventsUserComponent } from './events/events-user/events-user.component';
import { ImageSliderComponent } from './helpers/image-slider/image-slider.component';
import { SharedModule } from './shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { AngularFireModule } from "@angular/fire/compat";
import { AngularFireAuthModule } from "@angular/fire/compat/auth";
import { EmailConfirmationComponent } from './email/email-confirmation/email-confirmation.component';

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
    StayHomepageComponent,
    EventsUserComponent,
    ImageSliderComponent,
    EmailConfirmationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    SharedModule,
    NgxPaginationModule,
    AngularFireModule.initializeApp({
      apiKey: "AIzaSyCLhYY3ZNYRhfDhxsKjLOnh2ipEHKk0SWw",
      authDomain: "fir-angular-auth-7122a.firebaseapp.com",
      projectId: "fir-angular-auth-7122a",
      storageBucket: "fir-angular-auth-7122a.appspot.com",
      messagingSenderId: "103937755552",
      appId: "1:103937755552:web:b32b9ada92490f72d67cc5"
    })
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
