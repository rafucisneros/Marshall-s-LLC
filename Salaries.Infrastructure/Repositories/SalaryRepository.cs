using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
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
        public List<Salary> FindByEmployeeCode(string employeeCode, int page = 0, int pageSize = 20)
        {
            var query = from _salary in _context.Salaries
                        where _salary.EmployeeCode == employeeCode
                        select _salary;
            List<Salary> result = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        // Requirement 4
        public List<Salary> FindByGrade(int grade, int page = 0, int pageSize = 20)
        {
            var query = from _salary in _context.Salaries
                where _salary.Grade == grade
                select _salary;
            List<Salary> result = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        public List<Salary> FindByOfficeAndGrade(int officeId, int grade, int page = 0, int pageSize = 20)
        {
            var query = from _salary in _context.Salaries
                where _salary.OfficeId == officeId && _salary.Grade == grade
                select _salary;
            List<Salary> result = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        public List<Salary> FindByPositionAndGrade(int positionId, int grade, int page = 0, int pageSize = 20)
        {
            var query = from _salary in _context.Salaries
                        where _salary.PositionId == positionId && _salary.Grade == grade
                        select _salary;
            List<Salary> result = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }
    }
}
