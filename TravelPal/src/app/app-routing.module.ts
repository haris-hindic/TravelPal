import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './guards/admin.guard';
import { UserListComponent } from './admin/user-list/user-list.component';
import { AuthGuard } from './guards/auth.guard';
import { EventsCreationComponent } from './events/events-creation/events-creation.component';
import { EventsEditComponent } from './events/events-edit/events-edit.component';
import { EventsUserComponent } from './events/events-user/events-user.component';
import { EventsComponent } from './events/events.component';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './security/sign-in/sign-in.component';
import { SignUpComponent } from './security/sign-up/sign-up.component';
import { StayCreateComponent } from './stays/stay-create/stay-create.component';
import { StayDetailsComponent } from './stays/stay-details/stay-details.component';
import { StayEditComponent } from './stays/stay-edit/stay-edit.component';
import { StayHomepageComponent } from './stays/stay-search/stay-search.component';
import { StayUserComponent } from './stays/stay-user/stay-user.component';
import { StaysComponent } from './stays/stays.component';
import { EmailConfirmationComponent } from './email/email-confirmation/email-confirmation.component';
import { MyProfileComponent } from './user/my-profile/my-profile.component';
import { VerifiedGuard } from './guards/verified.guard';
import { StayReserveComponent } from './stays/reservations/stay-reserve/stay-reserve.component';
import { MessagesComponent } from './messages/messages.component';
import { UserReservationsComponent } from './stays/reservations/user-reservations/user-reservations.component';
import { ConversationComponent } from './messages/conversation/conversation.component';
import { EventsSignUpComponent } from './events/events-sign-up/events-sign-up.component';
import { EventsSignUpCreationComponent } from './events/events-sign-up/events-sign-up-creation/events-sign-up-creation.component';
import { HostReservationsComponent } from './stays/reservations/host-reservations/host-reservations.component';
import { InputNewPasswordComponent } from './security/forgot-password/input-new-password/input-new-password.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  //stays
  { path: 'stays', component: StaysComponent },
  { path: 'stays/search/:location', component: StayHomepageComponent },
  { path: 'stays/search', component: StayHomepageComponent },
  {
    path: 'user-stays/:id',
    component: StayUserComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'user-reservations',
    component: UserReservationsComponent,
    canActivate: [VerifiedGuard],
  },
  {
    path: 'host-reservations',
    component: HostReservationsComponent,
    canActivate: [VerifiedGuard],
  },
  {
    path: 'stays/create',
    component: StayCreateComponent,
    canActivate: [AuthGuard, VerifiedGuard],
  },
  {
    path: 'stays/details/:id',
    component: StayDetailsComponent,
  },
  {
    path: 'stays/edit/:id',
    component: StayEditComponent,
    canActivate: [AuthGuard, VerifiedGuard],
  },
  {
    path: 'stays/reserve/:id',
    component: StayReserveComponent,
    canActivate: [AuthGuard, VerifiedGuard],
  },
  //events
  { path: 'events', component: EventsComponent },
  {
    path: 'events/creation',
    component: EventsCreationComponent,
    canActivate: [AuthGuard, VerifiedGuard],
  },
  {
    path: 'events/signUp/:id',
    component: EventsSignUpComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'events/signUpCreation/:id',
    component: EventsSignUpCreationComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'events/edit/:id',
    component: EventsEditComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'user-events/:id',
    component: EventsUserComponent,
    canActivate: [AuthGuard],
  },
  //auth
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'forgot-password', component: SignInComponent },
  { path: 'input-new-password', component: InputNewPasswordComponent },

  {
    path: 'admin/users',
    component: UserListComponent,
    canActivate: [AdminGuard],
  },
  //profile
  {
    path: 'user/profile',
    component: MyProfileComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'authentication/emailconfirmation',
    component: EmailConfirmationComponent,
  },
  //messages
  {
    path: 'messages/:id',
    component: MessagesComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'messages/conversation/:id',
    component: ConversationComponent,
    canActivate: [AuthGuard],
  },

  // **
  { path: '**', redirectTo: '' },
];
@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
