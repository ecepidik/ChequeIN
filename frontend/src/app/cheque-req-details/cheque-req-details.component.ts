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

  response$: Observable<Object>;
  chequeStatusHist: Object;

  feedback: String;
  selectedStatus: String
  administratorApprover: String
  
  chequeReqId;

  ngOnInit(){
    let currentUrl = this.router.url;
    this.chequeReqId = (currentUrl.split('/'))[3];


    this.api.getChequeReqDetails(this.chequeReqId).subscribe(
      (chequeStatusHist$ : Object) =>
        { this.chequeStatusHist = chequeStatusHist$,
          console.log(this.chequeStatusHist)
      });
  }

  submitStatusUpdate() {
    var status = {
      feedback: this.feedback, 
      selectedStatus: this.selectedStatus, 
      administratorApprover: this.administratorApprover
    };

    this.api.postStatusUpdate(status, this.chequeReqId).subscribe (
      (res: Object ) =>
      console.log(res));
  }
}