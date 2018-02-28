import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { ChequeReq } from '../api/cheque-req';

@Component({
  selector: 'app-view-cheque-reqs',
  templateUrl: './view-cheque-reqs.component.html',
  styleUrls: ['./view-cheque-reqs.component.scss']
})
export class ViewChequeReqsComponent implements OnInit {
  chequeReqs$: Observable<ChequeReq[]>;

  constructor(public auth: AuthService, private api: ApiService) { }

  ngOnInit() {
    this.chequeReqs$ = this.api.getChequeReqs();
    this.chequeReqs$.subscribe((c) => {
      console.log(c);
    })
  }
}