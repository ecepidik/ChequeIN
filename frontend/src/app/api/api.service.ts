import {Injectable} from '@angular/core';
import {AuthHttp} from 'angular2-jwt';
import { Headers, RequestOptions, Response } from '@angular/http'; //Not sure if it is include in the AuthHttp
import {Observable} from 'rxjs/Rx';
import {environment} from '../../environments/environment';
import {ChequeReq} from './cheque-req';
import {User} from './user';
import {Account} from './account';
import {SubmittedChequeReq} from './submitted-cheque-req';

@Injectable()
export class ApiService {

  private chequeReqUrl = 'http://localhost:5000/api/chequereqs';

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
  submitChequeReq(chequeReq: ChequeReq){
    let body = JSON.stringify({ chequeReq });            
    let headers = new Headers({ 'Content-Type': 'application/json' });
    //let options = new RequestOptions({ headers: headers });

    return this.authHttp.post(this.chequeReqUrl, body)
        .map(this.extractData)
        .catch(this.handleError);
  }
  private extractData(res: Response) {
    let body = res.json();
    return body.data || {};
  }
  
  private handleError(error: Response) {
      console.error(error);
      return Observable.throw(error.json().error || 'Server Error');
  }

  
  }


}
