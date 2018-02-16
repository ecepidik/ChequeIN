import { Component, OnInit } from '@angular/core';
import { ChequeReq } from '../../app/api/cheque-req';
import { print } from 'util';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs/Observable';

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

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.accounts$ = this.api.getAccounts();
  }
}
