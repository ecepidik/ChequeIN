import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateChequeReqComponent } from './create-cheque-req/create-cheque-req.component';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './auth/auth.service';
import { LoginComponent } from './auth/login/login.component';
import { CallbackComponent } from './auth/callback/callback.component';
import { MenuComponent } from './menu/menu.component';
import { Http, RequestOptions, HttpModule } from '@angular/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';
import { ApiService } from './api.service';
import 'rxjs/Rx';

export function authHttpServiceFactory(http: Http, options: RequestOptions) {
  return new AuthHttp(
    new AuthConfig({
      tokenGetter: () => localStorage.getItem('access_token')
    }),
    http,
    options
  );
}

@NgModule({
  declarations: [
    AppComponent,
    CreateChequeReqComponent,
    HomeComponent,
    LoginComponent,
    CallbackComponent,
    MenuComponent
  ],

  imports: [AppRoutingModule, BrowserModule, HttpModule, NgbModule, FormsModule],
  providers: [
    AuthService,
    ApiService,
    {
      provide: AuthHttp,
      useFactory: authHttpServiceFactory,
      deps: [Http, RequestOptions]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
