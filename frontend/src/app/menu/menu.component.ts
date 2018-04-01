import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
})
export class MenuComponent implements OnInit {
  public isAdmin$: Observable<boolean>;

  constructor(public auth: AuthService) {}

  public ngOnInit() {
    this.isAdmin$ = this.auth.isAdmin();
  }
}
