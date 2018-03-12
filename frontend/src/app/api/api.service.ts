import { Injectable, Type } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { ChequeReq } from './cheque-req';
import { User } from './user';
import { Account } from './account';
import { SubmittedChequeReq } from './submitted-cheque-req';
import { Http, RequestOptions, HttpModule } from '@angular/http';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class ApiService {
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
      .map((res) => res.json())
      .map((accounts) => (Array.isArray(accounts) ? accounts : [accounts]));
  }

  /**
   * Submits a cheque req
   *
   * @param chequeReq The cheque req object to be submitted
   */
  async submitChequeReq(chequeReq: ChequeReq): Promise<void> {
    let uploadedDocuments = [];

    if (chequeReq.files instanceof File) {
      uploadedDocuments.push({
        Description: chequeReq.fileDescriptions[chequeReq.files.name],
        Base64Content: await getBase64(chequeReq.files)
      });
    } else {
      for (let i: number = 0; i < chequeReq.files.length; i++) {
        uploadedDocuments.push({
          Description: chequeReq.fileDescriptions[chequeReq.files[i].name],
          Base64Content: await getBase64(chequeReq.files[i])
        });
      }
    }

    let request = {
      freeFood: chequeReq.freeFood,
      onlinePurchases: chequeReq.onlinePurchase,
      toBeMailed: chequeReq.mailCheque,
      preTax: chequeReq.preTax,
      gst: chequeReq.GST,
      pst: chequeReq.PST,
      hst: chequeReq.HST,
      mailingAddress: {
        province: 1,
        line1: '3480 Rue University',
        line2: '',
        city: 'Montreal',
        postalCode: 'H3A 0E9'
      },
      UploadedDocuments: uploadedDocuments,
      ledgerAccountID: 1,
      payeeName: chequeReq.payableAddressee,
      description: chequeReq.description,
      approvedBy: chequeReq.approver
    };

    return this.authHttp
      .post(`${environment.apiUrl}/chequereqs`, request)
      .map((res) => res.json())
      .toPromise();
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
      .post(
        `${environment.apiUrl}/chequereqs/` + id + '/status',
        JSON.stringify(status),
        this.httpOptions
      )
      .map((res: Response) => res);
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
