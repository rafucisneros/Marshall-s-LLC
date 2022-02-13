using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
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
    }
}
