import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { ChequeReq2 } from '../api/cheque-req2';

@Component({
  selector: 'app-view-cheque-reqs',
  templateUrl: './view-cheque-reqs.component.html',
  styleUrls: ['./view-cheque-reqs.component.scss']
})
export class ViewChequeReqsComponent implements OnInit {
  chequeReqs$: Observable<[ChequeReq2]>;

  constructor(private api: ApiService) { }

  ngOnInit() {
    this.chequeReqs$ = this.api.getChequeReqs();
  }

}
