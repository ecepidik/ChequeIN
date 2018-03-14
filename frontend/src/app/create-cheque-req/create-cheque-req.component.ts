import { Component, OnInit } from '@angular/core';
import { ChequeReqSubmission } from '../../app/api/cheque-req-submission';
import { print } from 'util';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { FormControl, Validators } from '@angular/forms';

/**
 * This components contains the Cheque Req creation form.
 */
@Component({
  selector: 'app-create-cheque-req',
  templateUrl: './create-cheque-req.component.html',
  styleUrls: ['./create-cheque-req.component.scss'],
})
export class CreateChequeReqComponent implements OnInit {
  chequeReq: ChequeReqSubmission = new ChequeReqSubmission();
  accounts$: Observable<Account[]>;
  submitted: boolean = false;
  minPreTaxControl: FormControl;
  submissionResult$: Observable<string>;

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.accounts$ = this.api.getAccounts();
    this.minPreTaxControl = new FormControl('', Validators.min(0.01));
  }

  submitChequeReq() {
    this.submitted = true;
    this.submissionResult$ = Observable.fromPromise(this.api.submitChequeReq(this.chequeReq))
      .map(() => 'success')
      .catch(() => Observable.of('error'));
  }

  selectMultipleEvent(files: FileList | File): void {
    if (files instanceof File) {
      this.chequeReq.fileDescriptions[files.name] = '';
    } else {
      for (let i: number = 0; i < files.length; i++) {
        this.chequeReq.fileDescriptions[files[i].name] = '';
      }
    }
  }

  uploadMultipleEvent(files: FileList | File): void {}

  cancelMultipleEvent(): void {
    this.chequeReq.fileDescriptions = {};
  }

  isFile(files: FileList | File): boolean {
    return files instanceof File;
  }

  hasNaNCheck() {
    if (isNaN(this.chequeReq.preTax)) {
      this.chequeReq.preTax = 0;
      return true;
    } else if (isNaN(this.chequeReq.GST)) {
      this.chequeReq.GST = 0;
      return true;
    } else if (isNaN(this.chequeReq.PST)) {
      this.chequeReq.PST = 0;
      return true;
    } else if (isNaN(this.chequeReq.HST)) {
      this.chequeReq.HST = 0;
      return true;
    }
  }

  updateTotal() {
    let total =
      this.chequeReq.preTax + this.chequeReq.GST + this.chequeReq.PST + this.chequeReq.HST;
    return isNaN(total) ? 0 : total;
  }
}
