import { Component, OnInit } from '@angular/core';
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

  constructor() { }

  ngOnInit() {
  }

  cheque_description = '';
  description_error = false;
  
  updateDescription(value: string) { 
    this.cheque_description = value; 
    if(this.cheque_description == '') {
      this.description_error = true;
    } else {
      this.description_error = false;
    }
  }

  online_purchase = false;
  online_purchase_error = false;
  onlinePurchaseUpdate(value) {
    console.log(value);
    if (value == "Yes") {
      this.online_purchase = true;
      this.online_purchase_error = false;
    }
    else if (value == "No") {
      this.online_purchase = false;
      this.online_purchase_error = false;
    }
    else {
      this.online_purchase = false;
      this.online_purchase_error = true;
    }
  }
}
