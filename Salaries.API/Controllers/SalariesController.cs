using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
using Salaries.Domain.Exceptions;
using Salaries.Infrastructure.Repositories;

namespace Salaries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryRepository _repository;
        public SalariesController(ISalaryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        [Route("TotalSalaryReport")]
        public List<TotalSalary> TotalSalaryReport([FromBody] Dictionary<ReportFilter, string> filters=default, 
            [FromQuery] int page=1, [FromQuery] int pageSize=20)
        {
            List<TotalSalary> result = _repository.TotalSalariesReport(filters, page, pageSize);
            return result;
        }

        [HttpPost]
        [Route("AddOrUpdateSalary")]
        public Salary AddOrUpdateSalary([FromBody] Salary salary = default)
        {
            // If id is null, it is a new salary
            Salary result = null;
            try
            {
                if (salary.Id == 0)
                {
                    result = _repository.Add(salary);
                }
                else
                {
                    result = _repository.Update(salary);
                }
            }
            catch (SalariesDomainException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return result;
        }
    }
}
