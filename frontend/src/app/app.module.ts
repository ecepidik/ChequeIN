import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Http, HttpModule, RequestOptions } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CovalentFileModule } from '@covalent/core';
import { CovalentStepsModule } from '@covalent/core';
import { CovalentLayoutModule } from '@covalent/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthConfig, AuthHttp } from 'angular2-jwt';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import {
  CURRENCY_MASK_CONFIG,
  CurrencyMaskConfig,
} from 'ng2-currency-mask/src/currency-mask.config';
import 'rxjs/Rx';
import { ApiService } from './api/api.service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './auth/auth.service';
import { CallbackComponent } from './auth/callback/callback.component';
import { LoginComponent } from './auth/login/login.component';
import { ChequeReqDetailsComponent } from './cheque-req-details/cheque-req-details.component';
import { CreateChequeReqComponent } from './create-cheque-req/create-cheque-req.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { ViewChequeReqsComponent } from './view-cheque-reqs/view-cheque-reqs.component';
import { MaterialModule } from './material.module';
import { LedgersComponent } from './ledgers/ledgers.component';
import { LedgersDialog } from './ledgers/ledgers.component';

// Config for currency mask on dollar input fields
export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: 'right',
  allowNegative: true,
  decimal: '.',
  precision: 2,
  prefix: '$',
  suffix: '',
  thousands: ',',
};

export function authHttpServiceFactory(http: Http, options: RequestOptions) {
  return new AuthHttp(
    new AuthConfig({
      tokenGetter: () => localStorage.getItem('access_token'),
    }),
    http,
    options,
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
    ViewChequeReqsComponent,
    ChequeReqDetailsComponent,
    LedgersComponent,
    LedgersDialog,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpModule,
    HttpClientModule,
    FormsModule,
    MaterialModule,
    NgbModule,
    ReactiveFormsModule,
    CurrencyMaskModule,
    CovalentFileModule,
  ],
  entryComponents: [LedgersComponent, LedgersDialog],
  providers: [
    AuthService,
    ApiService,
    {
      provide: AuthHttp,
      useFactory: authHttpServiceFactory,
      deps: [Http, RequestOptions],
    },
    {
      provide: CURRENCY_MASK_CONFIG,
      useValue: CustomCurrencyMaskConfig,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
