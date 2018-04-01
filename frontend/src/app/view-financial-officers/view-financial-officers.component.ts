import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { ApiService } from '../api/api.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-view-financial-officers',
  templateUrl: './view-financial-officers.component.html',
  styleUrls: ['./view-financial-officers.component.scss']
})
export class ViewFinancialOfficersComponent implements OnInit {

  constructor(public auth: AuthService, private api: ApiService, private router: Router) { }


  public financialOfficerAccounts: Object;
  public ledgerAccounts: Object;

  ngOnInit() {
    this.api.getLedgerAccounts().subscribe((LedgerAccounts: any) => {
      console.log(LedgerAccounts)
    });

    this.api.getFinancialOfficer().subscribe((financialOfficerAccounts$: any) => {

      financialOfficerAccounts$.forEach(financialOfficer => {
        this.api.getLedgerAccountOfFinancialOfficer(financialOfficer.authenticationIdentifier).subscribe((financialOfficerDetails$: any) => {
          console.log(financialOfficerDetails$)
          financialOfficer.accounts = financialOfficerDetails$
        })
      });
      this.financialOfficerAccounts = financialOfficerAccounts$;

    });

    this.api.getAccounts().subscribe((ledgerAccounts$: any) => {
      this.ledgerAccounts = ledgerAccounts$;
    });

  }

  public addAccountToOfficer(officerId) {
    console.log(officerId)
    //this.api.postAddAccountToOfficer(accountId, officerId)
  }

}
