import {Injectable} from '@angular/core';
import {AuthHttp} from 'angular2-jwt';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../environments/environment';
import {ChequeReq} from './cheque-req';
import {User} from './user';
import {Account} from './account';
import {ChequeReq2} from './cheque-req2';

@Injectable()
export class ApiService {
  constructor(private authHttp: AuthHttp) {
  }

  /**
   * Gets information about the currently logged-in user.
   */
  getUser(): Observable<User> {
    return Observable.of({name: 'Jonh Doe'}); // TODO: actually call the API
  }

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

  /** 
   * Gets information about the submitted cheque reqs 
   */
   getChequeReqs(): Observable<Array<ChequeReq2>> {   //TODO: get multiple cheq reqs 
    return Observable.of(
      [
        {
          preTax: 15,
          GST: 1,
          PST: 1.33,
          HST: 0.66,
          description: 'ergrtgrt',
          onlinePurchase: '',
          approver: 'Jane Doe',
          account: Account | undefined,
          freeFood: false,
          mailCheque: false,
          mailingAddress: '123 Mountain Ave',
        },
        {
          preTax: 0,
          GST: 0,
          PST: 0,
          HST: 0,
          description: 'ergrtgrt',
          onlinePurchase: '',
          approver: 'Jane Doe',
          account: Account | undefined,
          freeFood: false,
          mailCheque: false,
          mailingAddress: '123 Mountain Ave',
        }
      ]); // TODO: actually call the API 
    }

  }
