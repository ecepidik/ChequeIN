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

    //Change the name of variable to match the back end
    const form = {
      onlinePurchases : chequeReq.onlinePurchase,
      toBeMailed : chequeReq.mailCheque,
      preTax : chequeReq.preTax,
      gst : chequeReq.GST,
      pst : chequeReq.PST,
      hst : chequeReq.HST,
      UploadedDocuments: [
        {
          "Description": "Report.pdf",
          "Base64Content": "hxhhGDB5576hhtT66D"
        }
      ],
      freeFood : chequeReq.freeFood,
      mailingAddress : {
        province : 1,
        line1 : "1645 rue des rigoles",
        line2 : "",
        city : "Sherb",
        postalCode : "J1M2H2"
        },
      description : chequeReq.description,
      approvedBy : chequeReq.approver,
      ledgerAccountID: 1,
      payeeName: "User"
    };
  
    let body = JSON.stringify(form);
    console.log(body);
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.authHttp
    .post(this.chequeReqUrl, body)
    .map((res) => res.json())
    .catch(this.handleError);
  }
  private handleError(error: Response) {
      console.error(error);
      return Observable.throw(error.json().error || 'Server Error');
  }
  getChequeReqs(): Observable<ChequeReq[]> {
    return this.authHttp
    .get(`${environment.apiUrl}/ChequeReqs`)
    .map((res) => res.json())
    .map((cheques) => (Array.isArray(cheques) ? cheques : [cheques]));
  }
}
