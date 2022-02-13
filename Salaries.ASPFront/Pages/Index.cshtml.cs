using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salaries.ASPFront.DTOs;
using Salaries.Infrastructure;
using Salaries.Infrastructure.Repositories;

namespace Salaries.ASPFront.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private SalaryRepository _repository;
        public SalariesDTO EmployeeInfo = null;

        [BindProperty]
        [FromForm]
        public string EmployeeCode { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public void OnPost()
        {
            var contextFactory = new SalariesContextDesignFactory();
            var _context = contextFactory.CreateDbContext(Array.Empty<string>());
            _repository = new SalaryRepository(_context);
            if (!string.IsNullOrWhiteSpace(EmployeeCode))
            {
                var salaries = _repository.FindByEmployeeCode(EmployeeCode);
                if (salaries?.Count > 0)
                {
                    var firstSalary = salaries.First();
                    EmployeeInfo = new SalariesDTO
                    {
                        EmployeeName = firstSalary.EmployeeName,
                        EmployeeSurname = firstSalary.EmployeeSurname,
                        EmployeeCode = firstSalary.EmployeeCode,
                        Birthday = firstSalary.Birthday,
                        IdentificationNumber = firstSalary.IdentificationNumber,
                        SalariesInfo = salaries.Select(salary =>
                        {
                            return new SalaryInfo
                            {
                                Office = salary.Office.Name,
                                Division = salary.Division.Name,
                                Position = salary.Position.Name,
                                BeginDate = salary.BeginDate,
                                Year = salary.Year,
                                Month = salary.Month,
                                BaseSalary = salary.BaseSalary,
                                ProductionBonus = salary.ProductionBonus,
                                CompensationBonus = salary.CompensationBonus,
                                Commission = salary.Commission,
                                Contributions = salary.Contributions
                            };
                        }).ToList()
                    };

                    // Calculate Employee Bonus
                    DateTime lastBeginDate =
                        EmployeeInfo.SalariesInfo.OrderByDescending(s => s.BeginDate).First().BeginDate;
                    EmployeeInfo.EmployeeBonus = salaries
                        .Take(3)
                        .Where(s => s.BeginDate == lastBeginDate)
                        .Sum(s => s.BaseSalary) / 3;
                }
            }
        }
    }
}
