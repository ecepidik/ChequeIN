import { Component, OnInit } from '@angular/core';
import {Approver, PayableAddressee} from '../cheque-req';

/**
 * This components contains the Cheque Req creation form.
 */
@Component({
  selector: 'app-create-cheque-req',
  templateUrl: './create-cheque-req.component.html',
  styleUrls: ['./create-cheque-req.component.scss']
})
export class CreateChequeReqComponent implements OnInit {

  approver: Approver = {
    name: ''
  };

  payableAddressee: PayableAddressee = {
    name: ''
  };

  constructor() { }

  ngOnInit() {

  }

}
