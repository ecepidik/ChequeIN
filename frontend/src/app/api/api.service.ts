import { Injectable, Type } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { ChequeReqSubmission } from './cheque-req-submission';
import { User } from './user';
import { Account } from './account';
import { SubmittedChequeReq } from './submitted-cheque-req';
import { Http, RequestOptions, HttpModule } from '@angular/http';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class ApiService {
  private chequeReqUrl = 'http://localhost:5000/api/chequereqs';
  constructor(private authHttp: AuthHttp, private http: HttpClient) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  /**
   * Gets information about the currently logged-in user.
   */
  getUser(): Observable<User> {
    return Observable.of({ name: 'Jonh Doe' }); // TODO: actually call the API
  }

  /**
   * Gets the list of accounts to which the current user has access.
   */
  getAccounts(): Observable<Account[]> {
    return this.authHttp
      .get(`${environment.apiUrl}/accounts`)
      .map((res) => res.json());
  }

  /**
   * Submits a cheque req
   *
   * @param chequeReq The cheque req object to be submitted
   */
  submitChequeReq(chequeReq: ChequeReqSubmission): Observable<void> {
    //Change the name of variable to match the back end
    const form = {
      onlinePurchases: chequeReq.onlinePurchase,
      toBeMailed: chequeReq.mailCheque,
      preTax: chequeReq.preTax,
      gst: chequeReq.GST,
      pst: chequeReq.PST,
      hst: chequeReq.HST,
      UploadedDocuments: [
        {
          Description: 'Report.pdf',
          Base64Content: 'hxhhGDB5576hhtT66D'
        }
      ],
      freeFood: chequeReq.freeFood,
      mailingAddress: {
        province: 1,
        line1: '1645 rue des rigoles',
        line2: '',
        city: 'Sherb',
        postalCode: 'J1M2H2'
      },
      description: chequeReq.description,
      approvedBy: chequeReq.approver,
      ledgerAccountID: 1,
      payeeName: chequeReq.payableAddressee
    };

    return this.authHttp
      .post(this.chequeReqUrl, form)
      .map((res) => res.json())
      .catch(this.handleError);
  }

  private handleError(error) {
    console.error(error);
    return Observable.throw(error || 'Server Error');
  }

  getChequeReqs(): Observable<SubmittedChequeReq[]> {
    return this.authHttp
      .get(`${environment.apiUrl}/ChequeReqs/`)
      .map((res) => res.json())
      .map((cheques) => (Array.isArray(cheques) ? cheques : [cheques]));
  }

  getChequeReqDetails(chequeReqId): Observable<Object> {
    return this.authHttp
      .get(`${environment.apiUrl}/chequereqs/` + chequeReqId + '/status')
      .map((cheques) => cheques.json());
  }

  postStatusUpdate(status, id): Observable<Object> {
    return this.authHttp.post(
      `${environment.apiUrl}/chequereqs/` + id + '/status',
      status
    );
  }
}

function getBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });
}
