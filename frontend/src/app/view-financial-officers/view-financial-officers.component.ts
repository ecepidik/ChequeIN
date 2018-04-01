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

  constructor(public auth: AuthService, private api: ApiService, private router: Router) {}


  public financialOfficerAccounts: Object;

  ngOnInit() {
    this.api.getLedgerAccounts().subscribe((LedgerAccounts: any) => {
      console.log(LedgerAccounts)
  });
  
  this.api.getFinancialOfficer().subscribe((financialOfficerAccounts$: any) => {
    // financialOfficerAccounts$[0].authorizedAccountGroups[0] = "ECSESS Exp";
    // financialOfficerAccounts$[0].authorizedAccountGroups[1] = "ECSESS Rev";

    // financialOfficerAccounts$[1].authorizedAccountGroups[0] = "MAME Exp";
    // financialOfficerAccounts$[1].authorizedAccountGroups[1] = "MAME Rev";
    
    financialOfficerAccounts$.forEach(financialOfficer => {
      this.api.getFinancialOfficerDetails(financialOfficer.authenticationIdentifier).subscribe((financialOfficerDetails$: any) => {
        console.log(financialOfficerDetails$.authorizedAccountGroups);
        financialOfficer.authorizedAccountGroups = financialOfficerDetails$.authorizedAccountGroups;
      })
      console.log(financialOfficer.authenticationIdentifier)
    });
    this.api.getFinancialOfficer
    this.financialOfficerAccounts = financialOfficerAccounts$;
    console.log(this.financialOfficerAccounts);
});



}

}
