import { Component, OnInit } from '@angular/core';

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


}
