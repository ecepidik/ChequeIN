import { CommonModule } from '@angular/common';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatDividerModule,
  MatIconModule,
  MatInputModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatSelectModule,
} from '@angular/material';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CovalentFileModule } from '@covalent/core';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api/api.service';
import { CreateChequeReqComponent } from './create-cheque-req.component';

describe('CreateChequeReqComponent', () => {
  let component: CreateChequeReqComponent;
  let fixture: ComponentFixture<CreateChequeReqComponent>;

  beforeEach(
    async(() => {
      const apiServiceMock: Partial<ApiService> = {
        getAccounts: () => {
          return Observable.of([
            {
              name: 'foo',
              number: 69,
            },
          ]);
        },
      };

      TestBed.configureTestingModule({
        imports: [
          CommonModule,
          FormsModule,
          ReactiveFormsModule,
          CovalentFileModule,
          MatButtonModule,
          MatCheckboxModule,
          MatDividerModule,
          MatIconModule,
          MatRadioModule,
          MatInputModule,
          MatProgressSpinnerModule,
          MatSelectModule,
          NoopAnimationsModule,
          CurrencyMaskModule,
        ],
        declarations: [CreateChequeReqComponent],
        providers: [{ provide: ApiService, useValue: apiServiceMock }],
      }).compileComponents();
    }),
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
