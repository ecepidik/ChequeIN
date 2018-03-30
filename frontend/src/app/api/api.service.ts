import { HttpHeaders } from '@angular/common/http';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { Http, HttpModule, RequestOptions } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Account } from './account';
import { ChequeReqSubmission } from './cheque-req-submission';
import { LedgerAcc } from '../../app/api/newLedger';
import { SubmittedChequeReq } from './submitted-cheque-req';
import { User } from './user';

@Injectable()
export class ApiService {
  constructor(private authHttp: AuthHttp) {}

  /**
   * Gets information about the currently logged-in user.
   */
  public getUser(): Observable<User> {
    return Observable.of({ name: 'Jonh Doe' }); // TODO: actually call the API
  }

  /**
   * Gets the list of accounts to which the current user has access.
   */
  public getAccounts(): Observable<Account[]> {
    return this.authHttp.get(`${environment.apiUrl}/accounts`).map(res => res.json());
  }

  /**
   * Submits a a new ledger
   *
   * @param newLedger The account object to be submitted
   */
  //TODO (Maxence Regaudie) : Write Submit to backend function
  public async createLedger(newLedger: LedgerAcc) : Promise<void>{
    const form = {
      name : newLedger.name,
      number : newLedger.number
  
    };
    
    return this.authHttp
      .post(`${environment.apiUrl}/accounts`, form)
      .map(res => res.json())
      .toPromise();
  }

  /**
   * Submits a cheque req
   *
   * @param chequeReq The cheque req object to be submitted
   */
  public async submitChequeReq(chequeReq: ChequeReqSubmission): Promise<void> {
    const uploadedDocuments = [];

    // TODO(Kareem): Clean this up
    if (chequeReq.files instanceof File) {
      uploadedDocuments.push({
        Description: chequeReq.fileDescriptions[chequeReq.files.name],
        Base64Content: await getBase64(chequeReq.files),
      });
    } else {
      for (let i: number = 0; i < chequeReq.files.length; i++) {
        uploadedDocuments.push({
          Description: chequeReq.fileDescriptions[chequeReq.files[i].name],
          Base64Content: await getBase64(chequeReq.files[i]),
        });
      }
    }

    // Change the name of variable to match the back end
    const form = {
      onlinePurchases: chequeReq.onlinePurchase,
      toBeMailed: chequeReq.mailCheque,
      preTax: chequeReq.preTax,
      gst: chequeReq.GST,
      pst: chequeReq.PST,
      hst: chequeReq.HST,
      UploadedDocuments: uploadedDocuments,
      freeFood: chequeReq.freeFood,
      // TODO: Put real data in here...
      mailingAddress: {
        province: 1,
        line1: '1645 rue des rigoles',
        line2: '',
        city: 'Sherb',
        postalCode: 'J1M2H2',
      },
      description: chequeReq.description,
      approvedBy: chequeReq.approver,
      ledgerAccountID: 1,
      payeeName: chequeReq.payableAddressee,
    };

    return this.authHttp
      .post(`${environment.apiUrl}/chequereqs`, form)
      .map(res => res.json())
      .toPromise();
  }

  public getChequeReqs(): Observable<SubmittedChequeReq[]> {
    return this.authHttp
      .get(`${environment.apiUrl}/ChequeReqs/`)
      .map(res => res.json());
  }

  // TODO(Ece): Make this query typed (use the real type instead of any)
  public getChequeReqDetails(chequeReqId): Observable<any> {
    return this.authHttp
      .get(`${environment.apiUrl}/chequereqs/${chequeReqId}/status`)
      .map(cheques => cheques.json());
  }

  // TODO(Ece): Make this query typed (use the real type instead of any)
  public postStatusUpdate(status, id): Observable<any> {
    return this.authHttp.post(`${environment.apiUrl}/chequereqs/${id}/status`, status);
  }
}

function getBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
  });
}
