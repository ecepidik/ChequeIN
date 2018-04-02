import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { ApiService } from '../api/api.service';
import { AuthService } from '../auth/auth.service';
import { LedgerAcc } from '../../app/api/newLedger';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

//Page that display current legdger and where you can click to create one
@Component({
  selector: 'app-ledgers',
  templateUrl: './ledgers.component.html',
  styleUrls: ['./ledgers.component.scss'],
})
export class LedgersComponent implements OnInit {
  public newAccount: LedgerAcc = new LedgerAcc();
  public accounts$: Observable<Account[]>;
  public submissionResult$: Observable<string>;

  constructor(private api: ApiService, public dialog: MatDialog) {}

  public ngOnInit() {
    this.accounts$ = this.api.getAccounts();
  }

  //Function to open Dialogue Function
  public openDialog(): void {
    let dialogRef = this.dialog.open(LedgersDialog);
  }
}

//Open window with the form for creating a new Ledger Account --------------------------------------------------------------------
@Component({
  selector: 'ledgers-dialog',
  templateUrl: './ledgers-dialog.html',
})
export class LedgersDialog {
  public newAccount: LedgerAcc = new LedgerAcc();
  public accounts$: Observable<Account[]>;
  public submitted: boolean = false;
  public AccForm: FormControl;
  public submissionResult$: Observable<string>;

  //TODO (Maxence Regaudie) : Clear the code of garbage line
  constructor(
    public dialogRef: MatDialogRef<LedgersDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private api: ApiService,
  ) {}

  public ngOnInit() {
    this.accounts$ = this.api.getAccounts();
  }

  public onNoClick(): void {
    this.dialogRef.close();
  }

  //Function to Create a new Ledger
  public createLedger() {
    console.log('Ledger Created!  ' + this.newAccount.name + ' || ' + this.newAccount.number);
    this.submitted = true;
    this.submissionResult$ = Observable.fromPromise(this.api.createLedger(this.newAccount))
      .map(() => 'success')
      .catch(() => Observable.of('error'));
  }
}
