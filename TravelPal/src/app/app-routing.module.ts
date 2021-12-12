import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsCreationComponent } from './events/events-creation/events-creation.component';
import { EventsDetailsComponent } from './events/events-details/events-details.component';
import { EventsEditComponent } from './events/events-edit/events-edit.component';
import { EventsComponent } from './events/events.component';
import { HomeComponent } from './home/home.component';
import { StayCreateComponent } from './stays/stay-create/stay-create.component';
import { StayDetailsComponent } from './stays/stay-details/stay-details.component';
import { StayEditComponent } from './stays/stay-edit/stay-edit.component';
import { StaysComponent } from './stays/stays.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'stays', component: StaysComponent },
  { path: 'stays/create', component: StayCreateComponent },
  { path: 'stays/details/:id', component: StayDetailsComponent },
  { path: 'stays/edit/:id', component: StayEditComponent },
  
  { path: 'events', component: EventsComponent},
  { path: 'events/details/:id', component: EventsDetailsComponent},
  { path: 'events/creation', component: EventsCreationComponent },
  { path: 'events/edit/:id', component: EventsEditComponent },
  {path: '**', redirectTo: ''}
];
@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
