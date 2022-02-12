using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Salaries.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Salaries.Domain.AggregatesModel.DivisionAggregate;
using Salaries.Domain.AggregatesModel.OfficeAggregate;
using Salaries.Domain.AggregatesModel.PositionAggregate;
using Salaries.Domain.AggregatesModel.SalaryAggregate;

namespace Salaries.API.Infrastructure
{
    public class SalariesContextSeed
    {
        public async Task SeedAsync(SalariesContext context, IWebHostEnvironment env, ILogger<SalariesContextSeed> logger)
        {
            await Task.Run(async () =>
            {
                using (context)
                {
                    //context.Database.Migrate();

                    #region Populate Offices Table
                    if (!context.Offices.Any())
                    {
                        var strings = new List<string> {"a"};
                        List<Office> offices = new List <Office>
                        {
                            new Office { Name = "Venezuela" },
                            new Office { Name = "Bolivia" },
                            new Office { Name = "Argentina" },
                            new Office { Name = "España" },
                            new Office { Name = "Ecuador" },
                        }
                        ;
                        context.Offices.AddRange(offices);
                        await context.SaveChangesAsync();
                    }
                    #endregion

                    #region Populate Division Table
                    if (!context.Divisions.Any())
                    {
                        var strings = new List<string> { "a" };
                        List<Division> divisions = new List<Division>
                            {
                                new Division { Name = "Customer Support" },
                                new Division { Name = "Operation" },
                                new Division { Name = "Marketing" },
                                new Division { Name = "Sales" },
                                new Division { Name = "IT" },
                            }
                            ;
                        context.Divisions.AddRange(divisions);
                        await context.SaveChangesAsync();
                    }
                    #endregion

                    #region Populate Positions Table
                    if (!context.Positions.Any())
                    {
                        var strings = new List<string> { "a" };
                        List<Position> positions = new List<Position>
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
                            }
                            ;
                        context.Positions.AddRange(positions);
                        await context.SaveChangesAsync();
                    }
                    #endregion

                    #region Populate Salaries Table
                    if (!context.Salaries.Any())
                    {
                        #region Sampling Sets for generating random employees
                        List<string> names = new List<string>
                        {
                            "Juan", "Pedro", "Maria", "Rafael", "Jose", "Ana", "Isabel", "Henry", "Tatiana",
                            "Mike", "James", "Jane", "Albert", "Nick", "Caroline", "Mariela", "Minaya", "Ricardo",
                            "Ariana", "Angel", "Giovana", "Johana", "Erika", "Francis", "Daniela", "Betty", "Jacqueline",
                            "Rosa", "William", "Jean", "David", "Monica"
                        };

                        List<string> surnames = new List<string>
                        {
                            "Barreto", "Infanti", "Ramirez", "Gonzalez", "Cisneros", "Olivares", "Rosales", "Franco", "Pereyra",
                            "Colmenares", "Nieto", "Duque", "Bolivar", "Carvajal", "Ramos", "Alcacer", "Puerta", "Casillas",
                            "Suarez", "Messi", "Navarro", "Reyes", "Hernandez", "Garcia", "Perez", "Lopez", "Teran", "Gomez",
                            "Araujo", "Vera", "Vargas", "Bravo"

                        };
                        #endregion

                        #region Generate 100 Employees
                        var random = new Random();
                        List<Salary> salaries = new List<Salary>();
                        foreach (var i in Enumerable.Range(0,100))
                        {
                            #region Generate fixed values for the new employee
                            // Pick a full name, code and identification number not used yet
                            string name = null;
                            string surname = null;
                            string identificationNumber = null;
                            string employeeCode = null;
                            bool invalidEmployee = true;
                            while (invalidEmployee)
                            {
                                name = names[random.Next(names.Count)];
                                surname = surnames[random.Next(surnames.Count)];
                                identificationNumber = random.Next(0, 99999999).ToString();
                                employeeCode = random.Next(0, 99999999).ToString();
                                if (!salaries.Any(employee =>
                                        (employee.EmployeeName == name && employee.EmployeeSurname == surname)
                                        || employee.IdentificationNumber == identificationNumber
                                        || employee.EmployeeCode == employeeCode
                                    ))
                                {
                                    invalidEmployee = false;
                                }
                            }

                            // Generate a birthdate
                            DateTime initBirthDate = new DateTime(1970, 1, 1);
                            DateTime birthDate = initBirthDate.AddDays(random.Next(10950)); // range of ~30 years
                            #endregion

                            int period = 0;
                            int periodYear = 0;
                            int periodMonth = 0;
                            int grade = 4; // Min grade
                            Position position = null;
                            int officeId = 0;
                            DateTime beginDate = birthDate;
                            decimal baseSalary = 0;
                            decimal productionBonus = 0;
                            decimal compensationBonus = 0;
                            decimal commissions = 0;
                            decimal contributions = 0;
                            bool calculateNewPeriod = true;
                            // Generate 6 months for the employee
                            while (period < 6)
                            {
                                #region Calculate period values
                                if (calculateNewPeriod)
                                {
                                    // Calculate period begin date
                                    if (beginDate == birthDate) // First time
                                    {
                                        periodYear = random.Next(birthDate.Year + 18, 2021);
                                        periodMonth = random.Next(1, 13);
                                        int beginDay = random.Next(1, 29);
                                        beginDate = new DateTime(periodYear, periodMonth, beginDay);
                                    }
                                    else // Other periods
                                    {
                                        int monthsLastPeriod = beginDate.Month <= periodMonth ? 
                                            periodMonth - beginDate.Month + 1: 12 - beginDate.Month + periodMonth + 1;
                                        beginDate = beginDate
                                            .AddMonths(monthsLastPeriod) // Sum months duration in the last period
                                            .AddMonths(random.Next(1, 20)); // Up tp 20 months of difference between periods
                                        periodYear = beginDate.Year;
                                        periodMonth = beginDate.Month;
                                    }
                                    // Pick a position
                                    position = context.Positions.OrderBy(position => Guid.NewGuid()).FirstOrDefault();

                                    // Pick an office
                                    officeId = context.Offices.OrderBy(office => Guid.NewGuid()).FirstOrDefault().Id;

                                    // Calculate new grade
                                    grade = random.Next(grade, 21); // Will only increase

                                    // Calculate salary, bonuses, etc...
                                    baseSalary = (random.Next(15, 60) * 100); // 1500 to 6000
                                    productionBonus = random.NextDouble() <= 0.4 ? 0: // 60% probabilities of getting bonus
                                        (random.Next(0, 40) * 100); // 0 to 4000
                                    compensationBonus = random.NextDouble() <= 0.1 ? 0 : // 90% probabilities of getting bonus
                                        (random.Next(0, 30) * 100); // 0 to 3000
                                    commissions = random.NextDouble() <= 0.6 ? 0 : // 40% probabilities of getting commissions
                                        (random.Next(0, 60) * 100); // 0 to 6000
                                    contributions = random.NextDouble() <= 0.1 ? 0 : // 90% probabilities of getting contributions
                                        (random.Next(0, 20) * 100); // 0 to 2000

                                    calculateNewPeriod = false;
                                }
                                else
                                {
                                    // New Year
                                    if (periodMonth == 12)
                                    {
                                        periodMonth = 1;
                                        periodYear++;
                                    }
                                    else
                                    // just next month
                                    {
                                        periodMonth++;
                                    }
                                }
                                #endregion
                                Salary salary = new Salary
                                {
                                    Year = periodYear,
                                    Month = periodMonth,
                                    Grade = grade,
                                    BeginDate = beginDate,
                                    EmployeeCode = employeeCode,
                                    EmployeeName = name,
                                    EmployeeSurname = surname,
                                    PositionId = position.Id,
                                    DivisionId = position.Division,
                                    OfficeId = officeId,
                                    Birthday = birthDate,
                                    IdentificationNumber = identificationNumber,
                                    BaseSalary = baseSalary,
                                    ProductionBonus = productionBonus,
                                    CompensationBonus = compensationBonus,
                                    Commission = commissions,
                                    Contributions = contributions
                                };
                                salaries.Add(salary);
                                period++;
                                if (random.NextDouble() <= 0.1) // 10% probabilities to change period
                                {
                                    calculateNewPeriod = true;
                                }
                            }
                        }
                        context.Salaries.AddRange(salaries);
                        await context.SaveChangesAsync();
                        #endregion
                    }
                    #endregion
                }
            });
        }
    }
}
