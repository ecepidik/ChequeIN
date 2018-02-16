import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateChequeReqComponent } from './create-cheque-req.component';

describe('CreateChequeReqComponent', () => {
  let component: CreateChequeReqComponent;
  let fixture: ComponentFixture<CreateChequeReqComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateChequeReqComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateChequeReqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});