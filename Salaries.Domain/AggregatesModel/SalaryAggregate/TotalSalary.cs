using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salaries.Domain.AggregatesModel.SalaryAggregate
{
    // DTO for TotalSalary
    public class TotalSalary
    {
        public string EmployeeCode { get; set; }
        public string EmployeeFullName { get; set; }
        public string Division { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }
        public string IdentificationNumber { get; set; }
        public decimal Salary { get; set; }
    }
}
