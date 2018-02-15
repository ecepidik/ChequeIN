import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { environment } from '../environments/environment';

@Injectable()
export class ApiService {
  constructor(private authHttp: AuthHttp) {}

  getValues(): Observable<any> {
    return this.authHttp
      .get(`${environment.apiUrl}/values`)
      .map((res) => res.json());
  }
}
