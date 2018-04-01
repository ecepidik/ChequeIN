import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { ApiService } from '../api/api.service';
import { SubmittedChequeReq } from '../api/submitted-cheque-req';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-view-cheque-reqs',
  templateUrl: './view-cheque-reqs.component.html',
  styleUrls: ['./view-cheque-reqs.component.scss'],
})
export class ViewChequeReqsComponent implements OnInit {
  // chequeReqs$: Observable<SubmittedChequeReq[]>;
  public chequeReqs: SubmittedChequeReq[];

  constructor(public auth: AuthService, private api: ApiService) {}

  public ngOnInit() {
    // this.chequeReqs$ = this.api.getChequeReqs();
    this.api.getChequeReqs().subscribe((chequeReqs$: SubmittedChequeReq[]) => {
      this.chequeReqs = chequeReqs$;
    });
  }
}
