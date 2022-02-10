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
import { StayHomepageComponent } from './stays/stay-search/stay-search.component';
import { EventsUserComponent } from './events/events-user/events-user.component';
import { ImageSliderComponent } from './helpers/image-slider/image-slider.component';
import { SharedModule } from './shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { EmailConfirmationComponent } from './email/email-confirmation/email-confirmation.component';
import { MyProfileComponent } from './user/my-profile/my-profile.component';
import { EditProfileComponent } from './user/my-profile/edit-profile/edit-profile.component';
import { EventsModalComponent } from './events/events-modal/events-modal.component';
import { VerifyComponent } from './user/my-profile/verify/verify.component';
import { ProfilePictureComponent } from './user/my-profile/profile-picture/profile-picture.component';
import { FooterComponent } from './footer/footer.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { StayReserveComponent } from './stays/reservations/stay-reserve/stay-reserve.component';
import { MessagesComponent } from './messages/messages.component';

import { UserReservationsComponent } from './stays/reservations/user-reservations/user-reservations.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    StaysComponent,
    EventsComponent,
    StayDetailsComponent,
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
    EmailConfirmationComponent,
    MyProfileComponent,
    EditProfileComponent,
    EventsModalComponent,
    VerifyComponent,
    ProfilePictureComponent,
    FooterComponent,
    StayReserveComponent,
    MessagesComponent,
    UserReservationsComponent,
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
    AngularEditorModule,
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
