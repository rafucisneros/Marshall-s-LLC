using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salaries.Domain.AggregatesModel.SalaryAggregate
{
    public enum ReportFilter
    {
        NoFilter,
        ByGrade,
        ByOffice,
        ByPosition
    }
}
