import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { CallbackComponent } from './auth/callback/callback.component';
import { LoginComponent } from './auth/login/login.component';
import { ChequeReqDetailsComponent } from './cheque-req-details/cheque-req-details.component';
import { CreateChequeReqComponent } from './create-cheque-req/create-cheque-req.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { ViewChequeReqsComponent } from './view-cheque-reqs/view-cheque-reqs.component';
import { ViewFinancialOfficersComponent } from './view-financial-officers/view-financial-officers.component';

const routes: Routes = [
  {
    // Used for the authentication process
    path: 'callback',
    component: CallbackComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    component: MenuComponent,
    canActivate: [AuthService],
    children: [
      { path: '', component: HomeComponent },
      { path: 'cheque-req/new', component: CreateChequeReqComponent },
      { path: 'cheque-req/view', component: ViewChequeReqsComponent },
      { path: 'cheque-req/statushistory/:chequeReqId', component: ChequeReqDetailsComponent },
      { path: 'financial-officer/view', component: ViewFinancialOfficersComponent },

    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
