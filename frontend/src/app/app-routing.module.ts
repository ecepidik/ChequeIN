import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateChequeReqComponent } from './create-cheque-req/create-cheque-req.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { AuthService } from './auth/auth.service';
import { CallbackComponent } from './auth/callback/callback.component';
import { LoginComponent } from './auth/login/login.component';

const routes: Routes = [
  {
    // Used for the authentication process
    path: 'callback',
    component: CallbackComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    component: MenuComponent,
    canActivate: [AuthService],
    children: [
      { path: '', component: HomeComponent },
      { path: 'cheque-req/new', component: CreateChequeReqComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}