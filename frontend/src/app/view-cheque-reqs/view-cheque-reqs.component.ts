import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { SubmittedChequeReq } from '../api/submitted-cheque-req';

@Component({
  selector: 'app-view-cheque-reqs',
  templateUrl: './view-cheque-reqs.component.html',
  styleUrls: ['./view-cheque-reqs.component.scss']
})
export class ViewChequeReqsComponent implements OnInit {
  chequeReqs$: Observable<[SubmittedChequeReq]>;

  constructor(private api: ApiService) { }

  ngOnInit() {
    // this.chequeReqs$ = this.api.getChequeReqs();
  }

}
