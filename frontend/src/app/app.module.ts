import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateChequeReqComponent } from './create-cheque-req/create-cheque-req.component';
import { HomeComponent } from './home/home.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthService } from './auth/auth.service';
import { LoginComponent } from './auth/login/login.component';
import { CallbackComponent } from './auth/callback/callback.component';
import { MenuComponent } from './menu/menu.component';
import { Http, RequestOptions, HttpModule } from '@angular/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';
import { ApiService } from './api/api.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatInputModule,
  MatIconModule,
  MatButton,
  MatDividerModule,
  MatSelectModule,
  MatExpansionModule,
  MatIcon
} from '@angular/material';
import 'rxjs/Rx';
import { ViewChequeReqsComponent } from './view-cheque-reqs/view-cheque-reqs.component';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CurrencyMaskConfig, CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CovalentLayoutModule } from '@covalent/core';
import { CovalentStepsModule } from '@covalent/core';
import { CovalentFileModule } from '@covalent/core';

// Config for currency mask on dollar input fields
export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: 'right',
  allowNegative: true,
  allowZero: true,
  decimal: '.',
  precision: 2,
  prefix: '$',
  suffix: '',
  thousands: ','
};

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
    MenuComponent,
    ViewChequeReqsComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpModule,
    FormsModule,
    MatButtonModule,
    MatDividerModule,
    MatCheckboxModule,
    MatInputModule,
    MatIconModule,
    MatSelectModule,
    MatExpansionModule,
    NgbModule,
    ReactiveFormsModule,
    CurrencyMaskModule,
    CovalentLayoutModule,
    CovalentStepsModule,
    CovalentFileModule
  ],
  providers: [
    AuthService,
    ApiService,
    {
      provide: AuthHttp,
      useFactory: authHttpServiceFactory,
      deps: [Http, RequestOptions]
    },
    {
      provide: CURRENCY_MASK_CONFIG,
      useValue: CustomCurrencyMaskConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
