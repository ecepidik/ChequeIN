import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { ChequeReq } from './cheque-req';

@Injectable()
export class ApiService {
  constructor(private authHttp: AuthHttp) {}

  /**
   * Gets the list of accounts to which the current user has access.
   */
  getAccounts(): Observable<Account[]> {
    return this.authHttp
      .get(`${environment.apiUrl}/accounts`)
      .map((res) => res.json())
      .map((accounts) => (Array.isArray(accounts) ? accounts : [accounts]));
  }

  /**
   * Submits a cheque req
   *
   * @param chequeReq The cheque req object to be submitted
   */
  submitChequeReq(chequeReq: ChequeReq): Observable<void> {
    return Observable.of(); // TODO: Make an actual API call
  }
}
