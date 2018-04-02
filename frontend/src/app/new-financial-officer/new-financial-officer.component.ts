import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { NewFinancialOfficer } from '../api/new-financial-officer';
import { ApiService } from '../api/api.service';

@Component({
  selector: 'app-new-financial-officer',
  templateUrl: './new-financial-officer.component.html',
  styleUrls: ['./new-financial-officer.component.scss']
})
export class NewFinancialOfficerComponent implements OnInit {

  public financialOfficer: NewFinancialOfficer = new NewFinancialOfficer();
  public submissionResult$: Observable<string>;
  public submitted: boolean = false;

  constructor(private api: ApiService) { }

  public ngOnInit() {
  }

  public submitNewOfficer() {
    this.submitted = true;
    this.financialOfficer.userType = "officer";
    this.submissionResult$ = this.api.submitNewOfficer(this.financialOfficer)
      .map(() => 'success')
      .catch(() => Observable.of('error'));
  }
}
