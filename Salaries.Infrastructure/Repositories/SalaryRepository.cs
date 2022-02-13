using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
using Salaries.Domain.Exceptions;
using Salaries.Domain.SharedKernel;

namespace Salaries.Infrastructure.Repositories
{
    public class SalaryRepository: ISalaryRepository
    {
        public readonly SalariesContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public SalaryRepository(SalariesContext context)
        {
            if (context != null)
            {
                _context = context;
            }
            else
            {
                throw new ArgumentNullException(nameof(context));
            }
        }

        public Salary Add(Salary salary)
        {
            Salary result = null;
            if (salary.IsValidSalary())
            {
                result = _context.Add(salary).Entity;
            }
            return result;
        }

        public Salary Update(Salary salary)
        {
            Salary result = null;
            if (salary.IsValidSalary())
            {
                result = _context.Update(salary).Entity;
            }
            return result;
        }

        public List<Salary> FindAll(int page=1, int pageSize=20)
        {
            var query = from _salary in _context.Salaries
                        select _salary;
            List<Salary> result = query
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        public Salary FindById(int id)
        {
            var query = from _salary in _context.Salaries
                        where _salary.Id == id
                        select _salary;
            Salary result = query.FirstOrDefault();
            return result;
        }

        // Requirment 5
        public List<Salary> FindByEmployeeCode(string employeeCode, int page = 1, int pageSize = 20)
        {
            var query = from _salary in _context.Salaries
                        where _salary.EmployeeCode == employeeCode
                        select _salary;
            List<Salary> result = query
                .Include(salary => salary.Division)
                .Include(salary => salary.Position)
                .Include(salary => salary.Office)
                .OrderByDescending(salary => salary.BeginDate)
                .ThenByDescending(salary => salary.Year)
                .ThenByDescending(salary => salary.Month)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        // Requirement 3 and 4
        public List<TotalSalary> TotalSalariesReport(Dictionary<ReportFilter, string> filters, int page = 1,
            int pageSize = 20)
        {
            
            var query = _context.Salaries
                .Include(salary => salary.Division)
                .Include(salary => salary.Position)
                .Include(salary => salary.Office)
                .AsQueryable();

            // Filters first for efficiency
            if (filters?.Count > 0)
            {
                foreach (KeyValuePair<ReportFilter, string> filter in filters)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Value))
                    {
                        switch (filter.Key)
                        {
                            case (ReportFilter.ByGrade):
                                query = query.Where(s => s.Grade == decimal.Parse(filter.Value));
                                break;
                            case (ReportFilter.ByOffice):
                                query = query.Where(s => s.Office.Name == filter.Value);
                                break;
                            case (ReportFilter.ByPosition):
                                query = query.Where(s => s.Position.Name == filter.Value);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        throw new SalariesDomainException("Null parameter for filtering not allowed");
                    }
                }
            }

            // Possibly inefficient
            return query
                .OrderBy(s => s.EmployeeCode)
                .ThenByDescending(s => s.BeginDate)
                .ThenByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .AsEnumerable()
                .GroupBy(s => s.EmployeeCode)
                .Select(g => g.FirstOrDefault())
                .Select(s => new TotalSalary
                    {
                        EmployeeCode = s.EmployeeCode,
                        EmployeeFullName = $"{s.EmployeeName} {s.EmployeeSurname}",
                        Office = s.Office.Name,
                        Division = s.Division.Name,
                        Position = s.Position.Name,
                        Grade = s.Grade,
                        BeginDate = s.BeginDate,
                        Birthday = s.Birthday,
                        IdentificationNumber = s.IdentificationNumber,
                        Salary = s.BaseSalary +
                                 s.ProductionBonus +
                                 ((decimal) 0.75 * s.CompensationBonus) +
                                 ((s.BaseSalary + s.Commission) * (decimal) 0.08 + s.Commission) + // other income
                                 -s.Contributions
                    }
                )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
