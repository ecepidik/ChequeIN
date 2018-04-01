import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import * as auth0 from 'auth0-js';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { AuthHttp } from 'angular2-jwt';

@Injectable()
export class AuthService implements CanActivate {
  public auth0 = new auth0.WebAuth({
    clientID: 'jVyq6jeftIbooUK9NYW9CUQ7VzLFCpOQ',
    domain: 'chequein-dev.auth0.com',
    responseType: 'token id_token',
    redirectUri: document.location.origin + '/callback',
    audience: 'https://quickstarts/api',
    scope: 'openid all',
  });

  constructor(public router: Router, private authHttp: AuthHttp) {}

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): void {
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';
        this.setSession(authResult);
        this.router.navigate(['/']);
      } else if (err) {
        this.router.navigate(['/']);

        // This error message is needed because it would be very hard to debug otherwise.
        // tslint:disable-next-line:no-console
        console.log(err);
      }
    });
  }

  private setSession(authResult): void {
    // Set the time that the Access Token will expire at
    const expiresAt = JSON.stringify(authResult.expiresIn * 1000 + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    // Go back to the home route
    this.router.navigate(['login']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // Access Token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    return new Date().getTime() < expiresAt;
  }

  public isAdmin(): Observable<boolean> {
    return this.authHttp
      .get(`${environment.apiUrl}/accounts`)
      .map(() => true)
      .catch(() => Observable.of(false));
  }

  /**
   * Tells the router is the user can access a route that's reserved to logged-in people
   */
  public canActivate() {
    if (this.isAuthenticated()) {
      return true;
    } else {
      // Redirect the user to the login page if they're not logged in.
      this.router.navigate(['login']);
      return false;
    }
  }
}
