import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { TotalSalary } from '../DTOs/TotalSalary';
import { Request } from '../DTOs/Request';

@Injectable({
  providedIn: 'root'
})
export class TotalSalariesReportService {
  private apiURL: string = "https://localhost:44384/api"

  constructor(private http: HttpClient) { }

  getTotalSalariesReport(options: Request = {} as Request): Observable<TotalSalary[]> {
    let { page=1, pageSize=10, filters={}} = options;
    return this.http.post<TotalSalary[]>(`${this.apiURL}/Salaries/TotalSalaryReport?page=${page}&pageSize=${pageSize}`, filters)
  }
}
