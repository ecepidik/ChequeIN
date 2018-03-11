import {Injectable, Type} from '@angular/core';
import {AuthHttp} from 'angular2-jwt';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../environments/environment';
import {ChequeReq} from './cheque-req';
import {User} from './user';
import {Account} from './account';
import {SubmittedChequeReq} from './submitted-cheque-req';
import { Http, RequestOptions, HttpModule } from '@angular/http';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class ApiService {
  constructor(private authHttp: AuthHttp, private http: HttpClient) {
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  "application/json"
    })
  };

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

  getChequeReqs(): Observable<Object[]> {
    return this.authHttp
    .get(`${environment.apiUrl}/ChequeReqs/`)
    .map((res) => res.json())
    .map((cheques) => (Array.isArray(cheques) ? cheques : [cheques]));
  }

  getChequeReqDetails(chequeReqId): Observable<Object> {
    return this.http
    .get(`${environment.apiUrl}/chequereqs/` + chequeReqId + '/status')
    .map((cheques) => (cheques ? cheques : null))
    .do(() => {
      console.log('request finished');
  });
  }

  postStatusUpdate(status, id): Observable<Object> {
    return this.http
    .post(`${environment.apiUrl}/chequereqs/`+ id + '/status', JSON.stringify(status), this.httpOptions)
    .map((res: Response) => res)
  }
}
