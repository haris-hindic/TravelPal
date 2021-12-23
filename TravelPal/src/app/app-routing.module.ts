import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './admin.guard';
import { UserListComponent } from './admin/user-list/user-list.component';
import { AuthGuardGuard } from './auth-guard.guard';
import { EventsCreationComponent } from './events/events-creation/events-creation.component';
import { EventsDetailsComponent } from './events/events-details/events-details.component';
import { EventsEditComponent } from './events/events-edit/events-edit.component';
import { EventsComponent } from './events/events.component';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './security/sign-in/sign-in.component';
import { SignUpComponent } from './security/sign-up/sign-up.component';
import { StayCreateComponent } from './stays/stay-create/stay-create.component';
import { StayDetailsComponent } from './stays/stay-details/stay-details.component';
import { StayEditComponent } from './stays/stay-edit/stay-edit.component';
import { StayHomepageComponent } from './stays/stay-homepage/stay-homepage.component';
import { StayUserComponent } from './stays/stay-user/stay-user.component';
import { StaysComponent } from './stays/stays.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'stays', component: StayHomepageComponent },
  { path: 'stays/search', component: StaysComponent },
  { path: 'user-stays/:id', component: StayUserComponent },
  {
    path: 'stays/create',
    component: StayCreateComponent,
    canActivate: [AuthGuardGuard],
  },
  {
    path: 'stays/details/:id',
    component: StayDetailsComponent,
  },
  {
    path: 'stays/edit/:id',
    component: StayEditComponent,
    canActivate: [AuthGuardGuard],
  },

  { path: 'events', component: EventsComponent },
  { path: 'events/details/:id', component: EventsDetailsComponent },
  { path: 'events/creation', component: EventsCreationComponent },
  { path: 'events/edit/:id', component: EventsEditComponent },

  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },

  {
    path: 'admin/users',
    component: UserListComponent,
    canActivate: [AdminGuard],
  },
  { path: '**', redirectTo: '' },
];
@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
