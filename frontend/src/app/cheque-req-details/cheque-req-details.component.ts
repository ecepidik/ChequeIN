import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { ApiService } from '../api/api.service';
import { SubmittedChequeReq } from '../api/submitted-cheque-req';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-cheque-req-details',
  templateUrl: './cheque-req-details.component.html',
  styleUrls: ['./cheque-req-details.component.scss'],
})
export class ChequeReqDetailsComponent implements OnInit {
  constructor(public auth: AuthService, private api: ApiService, private router: Router) {}

  public response$: Observable<Object>;
  public chequeStatusHist: Object;

  public feedback: string;
  public selectedStatus: string;
  public administratorApprover: string;

  public chequeReqId;

  public ngOnInit() {
    const currentUrl = this.router.url;
    this.chequeReqId = currentUrl.split('/')[3];

    // TODO(Ece): Make this code typed (use something else than any)
    this.api.getChequeReqDetails(this.chequeReqId).subscribe((chequeStatusHist: any) => {
      this.chequeStatusHist = chequeStatusHist;
    });
  }

  public submitStatusUpdate() {
    const status = {
      feedback: this.feedback,
      selectedStatus: this.selectedStatus,
      administratorApprover: this.administratorApprover,
    };

    this.api
      .postStatusUpdate(status, this.chequeReqId)
      .subscribe((res: Object) => console.log(res));
  }
}
