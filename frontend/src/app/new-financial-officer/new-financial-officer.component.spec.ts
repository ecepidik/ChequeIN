import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewFinancialOfficerComponent } from './new-financial-officer.component';

describe('NewFinancialOfficerComponent', () => {
  let component: NewFinancialOfficerComponent;
  let fixture: ComponentFixture<NewFinancialOfficerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewFinancialOfficerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewFinancialOfficerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
