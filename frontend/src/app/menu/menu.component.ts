import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ApiService } from '../api.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  values$: Observable<any>;

  constructor(public auth: AuthService, private api: ApiService) {}

  ngOnInit() {
    this.values$ = this.api.getValues();
  }
}
