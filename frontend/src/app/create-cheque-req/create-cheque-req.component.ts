import { Component, OnInit } from '@angular/core';
import { ChequeReq } from '../../app/api/cheque-req';
import { print } from 'util';

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

  constructor() {
    (window as any).t = this.chequeReq;
  }

  ngOnInit() {}
}
