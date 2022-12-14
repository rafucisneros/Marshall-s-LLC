using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salaries.Domain.SharedKernel;

namespace Salaries.Domain.AggregatesModel.SalaryAggregate
{
    public interface ISalaryRepository: IRepository<Salary>
    {
        Salary Add(Salary salary);
        Salary Update(Salary salary);
        Salary FindById(int id);
        List<Salary> FindAll(int page = 1, int pageSize = 20);

        // Requirement 3 and 4
        List<TotalSalary> TotalSalariesReport(Dictionary<ReportFilter, string> filters, int page = 1, int pageSize = 20);

        // Requirment 5
        List<Salary> FindByEmployeeCode(string employeeCode, int page = 1, int pageSize = 20);
    }
}
