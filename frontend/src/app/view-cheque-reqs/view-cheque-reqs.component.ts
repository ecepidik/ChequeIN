import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { ChequeReq } from '../api/cheque-req';
import { Account } from '../api/account';
import { SubmittedChequeReq } from '../api/submitted-cheque-req';

@Component({
  selector: 'app-view-cheque-reqs',
  templateUrl: './view-cheque-reqs.component.html',
  styleUrls: ['./view-cheque-reqs.component.scss']
})
export class ViewChequeReqsComponent implements OnInit {
  chequeReqs$: Observable<ChequeReq[]>;
  chequeReqs: Array<ChequeReq>=[];
  

  constructor(public auth: AuthService, private api: ApiService) { }

  ngOnInit() {
    
    this.chequeReqs$ = this.api.getChequeReqs();
    this.chequeReqs.push({
      "preTax": 0,
      "GST": 0,
      "PST": 0,
      HST: 0,
      description: 'cat food',
      onlinePurchase: false,
      payableAddressee: '',
      approver: 'VP Ca$$H',
      account: undefined,
      freeFood: false,
      mailCheque: false,
      mailingAddress: ''
    });
    this.chequeReqs$.subscribe((ChequeReqs) => {
      this.chequeReqs.push(ChequeReqs[0]);
      console.log(ChequeReqs);
    })
    console.log(this.chequeReqs);
  }
}