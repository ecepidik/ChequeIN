import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { User } from '../api/user';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
})
export class MenuComponent implements OnInit {
  public user$: Observable<User>;

  constructor(public auth: AuthService, private api: ApiService) {}

  public ngOnInit() {
    this.user$ = this.api.getUser();
  }
}
