import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { User } from '../api/user';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  user$: Observable<User>;

  constructor(public auth: AuthService, private api: ApiService) {}

  ngOnInit() {
    this.user$ = this.api.getUser();
  }
}
