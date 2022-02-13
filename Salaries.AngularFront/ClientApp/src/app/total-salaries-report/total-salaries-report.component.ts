import { Component, Injectable, OnInit } from '@angular/core';
import { TotalSalary } from '../DTOs/TotalSalary';
import { TotalSalariesReportService } from '../services/total-salaries-report.service';
import { Request } from '../DTOs/Request';

@Injectable()
@Component({
  selector: 'app-total-salaries-report',
  templateUrl: './total-salaries-report.component.html',
  styleUrls: ['./total-salaries-report.component.css']
})
export class TotalSalariesReportComponent implements OnInit {
  salaries: TotalSalary[];
  page: number;
  filters: any;
  selectedEmployee: string;

  constructor(private totalSalariesService: TotalSalariesReportService) { 
    this.salaries = [];
    this.page = 1;
    this.filters = {};
    this.selectedEmployee = "";
  }

  ngOnInit(): void {
    this.loadSalaries();
  }

  loadSalaries(): void {
    this.totalSalariesService.getTotalSalariesReport({
      page: this.page,
      pageSize: 5,
      filters: this.filters
    })
    .subscribe( data => {
      console.log(data)
      this.salaries = data
    })
  }

  selectEmployee(employeeCode: string): void {
    this.selectedEmployee = employeeCode;
  }

  nextPage(): void {
    this.page = this.page + 1;
    this.loadSalaries();
  }

  previousPage(): void {
    if(this.page > 1){
      this.page = this.page - 1;
      this.loadSalaries();
    }
  }

  clearFilters(): void {
    this.page = 1;
    this.filters = {};
    this.loadSalaries();
  }

  addFilters(filters: string[]): void {
    if(!this.selectedEmployee){
      alert("Debe seleccionar una empleado para realizar el filtro")
    } else {
      this.filters = {};
      let selectedEmployee = this.salaries.find(s => s.employeeCode == this.selectedEmployee);
      filters.forEach(filter => {
        switch(filter){
          case("ByGrade"):
            this.filters[filter] = selectedEmployee!["grade"].toString();
            break;
          case("ByOffice"):
            this.filters[filter] = selectedEmployee!["office"];
            break;
          case("ByPosition"):
            this.filters[filter] = selectedEmployee!["position"];
            break;
        }
      });
      this.loadSalaries();
    }
  }
}
