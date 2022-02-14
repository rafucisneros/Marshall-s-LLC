using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Salaries.Domain.AggregatesModel.DivisionAggregate;
using Salaries.Domain.AggregatesModel.OfficeAggregate;
using Salaries.Domain.AggregatesModel.PositionAggregate;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
using Salaries.Infrastructure;
using Salaries.Infrastructure.Repositories;
using Xunit;

namespace Salaries.Tests.Domain
{
    public class TotalSalariesReportTest
    {
        private SalaryRepository _repository;

        public TotalSalariesReportTest()
        {
            #region Mock Salary
            var salaries = new List<Salary>
            {
                new Salary
                {
                    Id = 1,
                    EmployeeCode = "100001123",
                    EmployeeName = "Rafael",
                    EmployeeSurname = "Cisneros",
                    Year = 2020,
                    Month = 12,
                    Office = new Office{Name = "Venezuela"},
                    Position = new Position{Name = "QA"},
                    Division = new Division{Name = "IT"},
                    Grade = 20,
                    BeginDate = new DateTime(2020, 1, 1),
                    Birthday = new DateTime(1995, 6, 23),
                    IdentificationNumber = "24759502",
                    BaseSalary = 1,
                    ProductionBonus = 1,
                    CompensationBonus = 1,
                    Commission = 1,
                    Contributions = 1
                },
                new Salary
                {
                    Id = 1,
                    EmployeeCode = "100001123",
                    EmployeeName = "William",
                    EmployeeSurname = "Suarez",
                    Year = 2020,
                    Month = 12,
                    Office = new Office{Name = "España"},
                    Position = new Position{Name = "QA"},
                    Division = new Division{Name = "IT"},
                    Grade = 18,
                    BeginDate = new DateTime(2020, 1, 1),
                    Birthday = new DateTime(1995, 6, 23),
                    IdentificationNumber = "24759502",
                    BaseSalary = 1,
                    ProductionBonus = 1,
                    CompensationBonus = 1,
                    Commission = 1,
                    Contributions = 1
                },
                new Salary
                {
                    Id = 1,
                    EmployeeCode = "100451123",
                    EmployeeName = "Pedro",
                    EmployeeSurname = "Alvarez",
                    Year = 2020,
                    Month = 12,
                    Office = new Office{Name = "Venezuela"},
                    Position = new Position{Name = "Seller"},
                    Division = new Division{Name = "Sales"},
                    Grade = 12,
                    BeginDate = new DateTime(2020, 1, 1),
                    Birthday = new DateTime(1995, 6, 23),
                    IdentificationNumber = "24759502",
                    BaseSalary = 1,
                    ProductionBonus = 1,
                    CompensationBonus = 1,
                    Commission = 1,
                    Contributions = 1
                },
                new Salary
                {
                    Id = 1,
                    EmployeeCode = "103331123",
                    EmployeeName = "Juana",
                    EmployeeSurname = "Martinez",
                    Year = 2020,
                    Month = 12,
                    Office = new Office{Name = "Venezuela"},
                    Position = new Position{Name = "QA"},
                    Division = new Division{Name = "IT"},
                    Grade = 20,
                    BeginDate = new DateTime(2020, 1, 1),
                    Birthday = new DateTime(1995, 6, 23),
                    IdentificationNumber = "24759502",
                    BaseSalary = 1,
                    ProductionBonus = 1,
                    CompensationBonus = 1,
                    Commission = 1,
                    Contributions = 1
                }
            }.AsQueryable();
            var mockSalaries = new Mock<DbSet<Salary>>();
            mockSalaries.As<IQueryable<Salary>>().Setup(m => m.Provider).Returns(salaries.Provider);
            mockSalaries.As<IQueryable<Salary>>().Setup(m => m.Expression).Returns(salaries.Expression);
            mockSalaries.As<IQueryable<Salary>>().Setup(m => m.ElementType).Returns(salaries.ElementType);
            mockSalaries.As<IQueryable<Salary>>().Setup(m => m.GetEnumerator()).Returns(salaries.GetEnumerator());
            #endregion

            #region Mock Offices
            var offices = new List<Office>
            {
                new Office { Name = "Venezuela" },
                new Office { Name = "Bolivia" },
                new Office { Name = "Argentina" },
                new Office { Name = "España" },
                new Office { Name = "Ecuador" },
            }.AsQueryable();
            var mockOffices = new Mock<DbSet<Office>>();
            mockOffices.As<IQueryable<Office>>().Setup(m => m.Provider).Returns(offices.Provider);
            mockOffices.As<IQueryable<Office>>().Setup(m => m.Expression).Returns(offices.Expression);
            mockOffices.As<IQueryable<Office>>().Setup(m => m.ElementType).Returns(offices.ElementType);
            mockOffices.As<IQueryable<Office>>().Setup(m => m.GetEnumerator()).Returns(offices.GetEnumerator());
            #endregion

            #region Mock Divisions
            var divisions = new List<Division>
            {
                new Division { Name = "Customer Support" },
                new Division { Name = "Operation" },
                new Division { Name = "Marketing" },
                new Division { Name = "Sales" },
                new Division { Name = "IT" },
            }.AsQueryable();
            var mockDivisions = new Mock<DbSet<Division>>();
            mockDivisions.As<IQueryable<Division>>().Setup(m => m.Provider).Returns(divisions.Provider);
            mockDivisions.As<IQueryable<Division>>().Setup(m => m.Expression).Returns(divisions.Expression);
            mockDivisions.As<IQueryable<Division>>().Setup(m => m.ElementType).Returns(divisions.ElementType);
            mockDivisions.As<IQueryable<Division>>().Setup(m => m.GetEnumerator()).Returns(divisions.GetEnumerator());
            #endregion

            #region Mock Positions
            var positions = new List<Position>
            {
                // Customer Support
                new Position { Name = "Director", Division = 1 },
                new Position { Name = "Sales", Division = 1 },
                new Position { Name = "Assistant", Division = 1 },
                new Position { Name = "Secretary", Division = 1 },
                new Position { Name = "IT", Division = 1 },

                // Operation
                new Position { Name = "Cargo Manager", Division = 2 },
                new Position { Name = "Head of Cargo", Division = 2 },
                new Position { Name = "Cargo Assistant", Division = 2 },

                // Marketing
                new Position { Name = "Director of Marketing", Division = 3 },
                new Position { Name = "Designer", Division = 3 },
                new Position { Name = "Community Manager", Division = 3 },
                new Position { Name = "UI/UX", Division = 3 },

                // Sales
                new Position { Name = "Sales Manager", Division = 4 },
                new Position { Name = "Account Executive", Division = 4},
                new Position { Name = "Seller", Division = 4 },
                new Position { Name = "Assistant", Division = 4 },

                // IT
                new Position { Name = "Developer", Division = 5 },
                new Position { Name = "Database Manager", Division = 5},
                new Position { Name = "QA", Division = 5 },
                new Position { Name = "Project Manager", Division = 5 },
                new Position { Name = "Director", Division = 5 },
            }.AsQueryable();
            var mockPositions = new Mock<DbSet<Position>>();
            mockPositions.As<IQueryable<Position>>().Setup(m => m.Provider).Returns(positions.Provider);
            mockPositions.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(positions.Expression);
            mockPositions.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(positions.ElementType);
            mockPositions.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(positions.GetEnumerator());
            #endregion

            #region Mock Context
            var mockContext = new Mock<SalariesContext>();
            mockContext.Setup(c => c.Divisions).Returns(mockDivisions.Object);
            mockContext.Setup(c => c.Positions).Returns(mockPositions.Object);
            mockContext.Setup(c => c.Offices).Returns(mockOffices.Object);
            mockContext.Setup(c => c.Salaries).Returns(mockSalaries.Object);
            #endregion

            this._repository = new SalaryRepository(mockContext.Object);
        }
        [Fact]
        public void Filter_By_Grade()
        {
            string grade = "20";
            Dictionary<ReportFilter, string> filters = new Dictionary<ReportFilter, string>
            {
                {ReportFilter.ByGrade, grade}
            };
            var salaries = this._repository.TotalSalariesReport(filters);
            Assert.True(salaries.All(s => s.Grade == decimal.Parse(grade)));
        }
        [Fact]
        public void Filter_By_Office()
        {
            string office = "Venezuela";
            Dictionary<ReportFilter, string> filters = new Dictionary<ReportFilter, string>
            {
                {ReportFilter.ByOffice, office}
            };
            var salaries = this._repository.TotalSalariesReport(filters);
            Assert.True(salaries.All(s => s.Office == office));
        }
        [Fact]
        public void Filter_By_Position()
        {
            string position = "QA";
            Dictionary<ReportFilter, string> filters = new Dictionary<ReportFilter, string>
            {
                {ReportFilter.ByPosition, position}
            };
            var salaries = this._repository.TotalSalariesReport(filters);
            Assert.True(salaries.All(s => s.Position == position));
        }
    }
}
