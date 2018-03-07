import { Component, OnInit } from '@angular/core';
import { ChequeReq } from '../../app/api/cheque-req';
import { print } from 'util';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';

/**
 * This components contains the Cheque Req creation form.
 */
@Component({
  selector: 'app-create-cheque-req',
  templateUrl: './create-cheque-req.component.html',
  styleUrls: ['./create-cheque-req.component.scss']
})
export class CreateChequeReqComponent implements OnInit {
  chequeReq: ChequeReq = new ChequeReq();
  accounts$: Observable<Account[]>;
  submitted: boolean = false;


  constructor(private api: ApiService) {}

  ngOnInit() { this.accounts$ = this.api.getAccounts();}

  submitChequeReq() {
    this.submitted = true;

    console.log("Submitted: ", this.chequeReq); // TODO: actually submit the cheque req
  }
}
