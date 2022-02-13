import { TestBed } from '@angular/core/testing';

import { TotalSalariesReportService } from './total-salaries-report.service';

describe('TotalSalariesReportService', () => {
  let service: TotalSalariesReportService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TotalSalariesReportService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
