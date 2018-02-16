import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';

@Injectable()
export class ApiService {
  constructor(private authHttp: AuthHttp) {}

  getAccounts(): Observable<Account[]> {
    return this.authHttp
      .get(`${environment.apiUrl}/accounts`)
      .map((res) => res.json())
      .map((accounts) => (Array.isArray(accounts) ? accounts : [accounts]));
  }
}
