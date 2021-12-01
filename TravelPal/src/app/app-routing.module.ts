import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsDetailsComponent } from './events/events-details/events-details.component';
import { EventsComponent } from './events/events.component';
import { HomeComponent } from './home/home.component';
import { StayDetailsComponent } from './stays/stay-details/stay-details.component';
import { StaysComponent } from './stays/stays.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },

  { path: 'stays', component: StaysComponent },
  { path: 'stays/details/:id', component: StayDetailsComponent },

  { path: 'events', component: EventsComponent}
  { path: 'events/details/:id', component: EventsDetailsComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
