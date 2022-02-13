using System;
using System.ComponentModel.DataAnnotations;
using Salaries.Domain.AggregatesModel.DivisionAggregate;
using Salaries.Domain.AggregatesModel.OfficeAggregate;
using Salaries.Domain.AggregatesModel.PositionAggregate;
using Salaries.Domain.Exceptions;

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

        public bool IsValidSalary()
        {
            bool result = true;
            if (
                this.BaseSalary <= 0
                || this.ProductionBonus <= 0
                || this.CompensationBonus <= 0
                || this.Commission <= 0
                || this.Contributions <= 0
            )
            {
                throw new SalariesDomainException("Invalid value for a salary, bonuses, commission or contributions");
            }

            if (this.BeginDate < this.Birthday)
            {
                throw new SalariesDomainException("Begin Date cannot be prior to Birth Date");
            }

            if (string.IsNullOrWhiteSpace(this.EmployeeCode))
            {
                throw new SalariesDomainException("Employee's Employee Code must be specified.");
            }

            if (string.IsNullOrWhiteSpace(this.EmployeeName))
            {
                throw new SalariesDomainException("Employee's name must be specified.");
            }

            if (string.IsNullOrWhiteSpace(this.EmployeeSurname))
            {
                throw new SalariesDomainException("Employee's surname must be specified.");
            }

            if (string.IsNullOrWhiteSpace(this.IdentificationNumber))
            {
                throw new SalariesDomainException("Employee's identification number must be specified.");
            }

            if (Grade < 0)
            {
                throw new SalariesDomainException("Grade must be greater than 0.");
            }

            if (Year < 0 && Month < 0)
            {
                throw new SalariesDomainException("Year and Month of the salary must be greater than 0.");
            }

            if (Year < this.BeginDate.Year)
            {
                throw new SalariesDomainException("Year of the salary cannot be prior to the starting Year for the position.");
            }
            return result;
        }
    }
}
