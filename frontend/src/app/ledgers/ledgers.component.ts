import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Account } from '../api/account';
import { ApiService } from '../api/api.service';
import { AuthService } from '../auth/auth.service';


@Component({
  selector: 'app-ledgers',
  templateUrl: './ledgers.component.html',
  styleUrls: ['./ledgers.component.scss']
})
export class LedgersComponent implements OnInit {
  //Temporarly TODO: Import with Service
  data = [
    {
       item: "John - 12232"
    },
    {
       item: "alf - 39092"
    },
    {
       item: "louis - 96324"
    }
 ]



  constructor() {}
  

  ngOnInit() {
  }

}
