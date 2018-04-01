import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { print } from 'util';
import { ChequeReqSubmission } from '../../app/api/cheque-req-submission';
import { Account } from '../api/account';
import { ApiService } from '../api/api.service';

/**
 * This components contains the Cheque Req creation form.
 */
@Component({
  selector: 'app-create-cheque-req',
  templateUrl: './create-cheque-req.component.html',
  styleUrls: ['./create-cheque-req.component.scss'],
})
export class CreateChequeReqComponent implements OnInit {
  public chequeReq: ChequeReqSubmission = new ChequeReqSubmission();
  public accounts$: Observable<Account[]>;
  public submitted: boolean = false;
  public minPreTaxControl: FormControl;
  public submissionResult$: Observable<string>;

  constructor(private api: ApiService) {}

  public ngOnInit() {
    this.accounts$ = this.api.getAccounts();
    this.minPreTaxControl = new FormControl('', Validators.min(0.01));
  }

  public submitChequeReq() {
    this.submitted = true;
    this.submissionResult$ = Observable.fromPromise(this.api.submitChequeReq(this.chequeReq))
      .map(() => 'success')
      .catch(() => Observable.of('error'));
  }

  public selectMultipleEvent(files: FileList | File): void {
    if (files instanceof File) {
      this.chequeReq.fileDescriptions[files.name] = '';
    } else {
      for (let i: number = 0; i < files.length; i++) {
        this.chequeReq.fileDescriptions[files[i].name] = '';
      }
    }
  }

  public uploadMultipleEvent(files: FileList | File): void {}

  public cancelMultipleEvent(): void {
    this.chequeReq.fileDescriptions = {};
  }

  public isFile(files: FileList | File): boolean {
    return files instanceof File;
  }

  public hasNaNCheck() {
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

  public updateTotal() {
    const total =
      this.chequeReq.preTax + this.chequeReq.GST + this.chequeReq.PST + this.chequeReq.HST;
    return isNaN(total) ? 0 : total;
  }
}
