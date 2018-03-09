import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { ChequeReq } from '../api/cheque-req';
import { Account } from '../api/account';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cheque-req-details',
  templateUrl: './cheque-req-details.component.html',
  styleUrls: ['./cheque-req-details.component.scss']
})
export class ChequeReqDetailsComponent implements OnInit {

  constructor(public auth: AuthService, private api: ApiService, private router: Router) { }

  ngOnInit(){
    console.log("HERE")
    let currentUrl = this.router.url;
    let chequeReqId = (currentUrl.split('/'))[2];
    console.log(chequeReqId);

    this.api.getChequeReqDetails(chequeReqId).subscribe(
      (chequeReqDetails$ : Object) =>
        console.log(chequeReqDetails$))

  }
}