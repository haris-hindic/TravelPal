import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { StaysComponent } from './stays/stays.component';
import { EventsComponent } from './events/events.component';
import { AppRoutingModule } from './app-routing.module';
import { StayDetailsComponent } from './stays/stay-details/stay-details.component';
import { EventsDetailsComponent } from './events/events-details/events-details.component';
import { NgImageSliderModule } from 'ng-image-slider';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    StaysComponent,
    EventsComponent,
    StayDetailsComponent,
    EventsDetailsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgImageSliderModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
