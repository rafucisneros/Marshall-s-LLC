using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salaries.ASPFront.DTOs
{
    public class SalariesDTO
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public DateTime Birthday { get; set; }
        public string IdentificationNumber { get; set; }
        public decimal EmployeeBonus { get; set; }
        public List<SalaryInfo> SalariesInfo { get; set; }
    }

    public class SalaryInfo
    {
        public string Division { get; set; }
        public string Position { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Office { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal CompensationBonus { get; set; }
        public decimal ProductionBonus { get; set; }
        public decimal Commission { get; set; }
        public decimal Contributions { get; set; }
    }
}
