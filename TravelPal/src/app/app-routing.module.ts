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

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  //stays
  { path: 'stays', component: StaysComponent },
  { path: 'stays/search/:location', component: StayHomepageComponent },
  { path: 'stays/search', component: StayHomepageComponent },
  { path: 'user-stays/:id', component: StayUserComponent },
  {
    path: 'stays/create',
    component: StayCreateComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'stays/details/:id',
    component: StayDetailsComponent,
  },
  {
    path: 'stays/edit/:id',
    component: StayEditComponent,
    canActivate: [AuthGuard],
  },
  //events
  { path: 'events', component: EventsComponent },
  {
    path: 'events/creation',
    component: EventsCreationComponent,
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

  {
    path: 'admin/users',
    component: UserListComponent,
    canActivate: [AdminGuard],
  },
  //profile
  { path: 'user/profile', component: MyProfileComponent },
  {
    path: 'authentication/emailconfirmation',
    component: EmailConfirmationComponent,
  },
  { path: '**', redirectTo: '' },
];
@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
