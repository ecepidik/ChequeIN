import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewFinancialOfficersComponent } from './view-financial-officers.component';

describe('ViewFinancialOfficersComponent', () => {
  let component: ViewFinancialOfficersComponent;
  let fixture: ComponentFixture<ViewFinancialOfficersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewFinancialOfficersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewFinancialOfficersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
