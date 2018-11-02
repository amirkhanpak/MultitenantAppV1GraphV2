import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { MsalModule, MsalGuard, MsalInterceptor, MsalService } from '@azure/msal-angular';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MsalModule.forRoot({
      authority: 'https://login.microsoftonline.com/lorenzoshellscalemaca.onmicrosoft.com',
      //consentScopes: ['api://cfa9c78c-aaa6-462a-9242-0272e60823b6/access_as_user'],
     // clientID: 'e1fa9988-5dab-4dab-9941-529376b68dc2',
      clientID: 'f99d8651-c102-424a-bd7e-cca5d958ae34',
     // redirectUri: 'https://azuremsalwebapp.extranet.caloes.ca.gov/'
      //redirectUri: 'https://multitenantappb2cv1.extranet.caloes.ca.gov/'
      //popUp: true,
      //protectedResourceMap: protectedResourceMap,
      //postLogoutRedirectUri: "https://localhost:44356/",
      //logger: loggerCallback,
      //level: LogLevel.Verbose
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent, canActivate: [MsalGuard] },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [MsalGuard]},
    ])
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: MsalInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
