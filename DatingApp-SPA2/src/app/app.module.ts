import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';


import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import {
  AgmCoreModule
} from '@agm/core';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoaderRequestInterceptor } from './components/_services/loader-request-interceptor';
import { NgxUiLoaderModule, NgxUiLoaderConfig } from 'ngx-ui-loader';
export function tokenGetter() {
  return localStorage.getItem('token');
}

const configLoader : NgxUiLoaderConfig  =
{
  bgsColor: "#3590c4",
  bgsOpacity: 0.5,
  bgsPosition: "bottom-right",
  bgsSize: 100,
  bgsType: "three-strings",
  blur: 5,
  fgsColor: "#9a3e65",
  fgsPosition: "center-center",
  fgsSize: 100,
  fgsType: "three-strings",
  gap: 24,
  logoPosition: "bottom-right",
  logoSize: 55,
  masterLoaderId: "master",
  overlayBorderRadius: "0",
  overlayColor: "rgba(234,234,234,0.64)",
  pbColor: "red",
  pbDirection: "ltr",
  pbThickness: 2,
  hasProgressBar: true,
  
}
@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'YOUR_GOOGLE_MAPS_API_KEY'
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      },
    }),
    NgxUiLoaderModule.forRoot(configLoader)
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,

  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoaderRequestInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
