import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { SalaryFormComponent } from './salary-form/salary-form.component';
import { TotalSalariesReportComponent } from './total-salaries-report/total-salaries-report.component';
import { TotalSalariesReportService } from './services/total-salaries-report.service';

@NgModule({
  declarations: [
    AppComponent,
    SalaryFormComponent,
    TotalSalariesReportComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [TotalSalariesReportService],
  bootstrap: [AppComponent]
})
export class AppModule { }
