import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateChequeReqComponent } from './create-cheque-req.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatDividerModule,
  MatInputModule,
  MatSelectModule,
  MatRadioModule,
  MatIconModule
} from '@angular/material';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs/Observable';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { CovalentFileModule, CovalentStepsModule, CovalentLayoutModule } from '@covalent/core';

describe('CreateChequeReqComponent', () => {
  let component: CreateChequeReqComponent;
  let fixture: ComponentFixture<CreateChequeReqComponent>;

  beforeEach(
    async(() => {
      let apiServiceMock: Partial<ApiService> = {
        getAccounts: () => {
          return Observable.of([
            {
              name: 'foo',
              number: 69
            }
          ]);
        }
      };

      TestBed.configureTestingModule({
        imports: [
          CommonModule,
          FormsModule,
          ReactiveFormsModule,
          CovalentLayoutModule,
          CovalentStepsModule,
          CovalentFileModule,
          MatButtonModule,
          MatCheckboxModule,
          MatDividerModule,
          MatIconModule,
          MatRadioModule,
          MatInputModule,
          MatSelectModule,
          NoopAnimationsModule,
          CurrencyMaskModule
        ],
        declarations: [CreateChequeReqComponent],
        providers: [{ provide: ApiService, useValue: apiServiceMock }]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateChequeReqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
