import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TotalSalariesReportComponent } from './total-salaries-report.component';

describe('TotalSalariesReportComponent', () => {
  let component: TotalSalariesReportComponent;
  let fixture: ComponentFixture<TotalSalariesReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TotalSalariesReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TotalSalariesReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
