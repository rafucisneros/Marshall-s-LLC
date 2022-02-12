using System;
using System.ComponentModel.DataAnnotations;
using Salaries.Domain.AggregatesModel.DivisionAggregate;
using Salaries.Domain.AggregatesModel.OfficeAggregate;
using Salaries.Domain.AggregatesModel.PositionAggregate;

namespace Salaries.Domain.AggregatesModel.SalaryAggregate
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }

        [StringLength(10)]
        public string EmployeeCode { get; set; }

        [StringLength(150)]
        public string EmployeeName { get; set; }

        [StringLength(150)]
        public string EmployeeSurname { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(10)]
        public string IdentificationNumber { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal CompensationBonus { get; set; }
        public decimal ProductionBonus { get; set; }
        public decimal Commission { get; set; }
        public decimal Contributions { get; set; }
    }
}
