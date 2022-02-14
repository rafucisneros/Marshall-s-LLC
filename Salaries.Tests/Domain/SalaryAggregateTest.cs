using System;
using Xunit;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
using Salaries.Domain.Exceptions;

namespace Salaries.Tests.Domain
{
    public class SalaryAggregateTest
    {
        [Fact]
        public void Create_Valid_Salary()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 1,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.True(salary.IsValidSalary());
        }

        [Fact]
        public void Invalid_Base_Salary()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = -10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }

        [Fact]
        public void Missing_Employee_Code()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }

        [Fact]
        public void Begin_Date_Prior_Birth_Date()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(1990, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Missing_Name()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = -10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Missing_Surname()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Invalid_Grade()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = -20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Invalid_Month()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = -12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Invalid_Year()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = -10,
                Month = 12,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Salary_Year_Prior_Begin_Date()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2015,
                Month = 7,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 1, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
        [Fact]
        public void Salary_Month_Prior_Begin_Date()
        {
            Salary salary = new Salary
            {
                EmployeeCode = "100001123",
                EmployeeName = "Rafael",
                EmployeeSurname = "Cisneros",
                Year = 2020,
                Month = 7,
                OfficeId = 1,
                PositionId = 1,
                DivisionId = 1,
                Grade = 20,
                BeginDate = new DateTime(2020, 10, 1),
                Birthday = new DateTime(1995, 6, 23),
                IdentificationNumber = "24759502",
                BaseSalary = 10,
                ProductionBonus = 1,
                CompensationBonus = 1,
                Commission = 1,
                Contributions = 1
            };
            Assert.Throws<SalariesDomainException>(() =>
            {
                salary.IsValidSalary();
            });
        }
    }
}
