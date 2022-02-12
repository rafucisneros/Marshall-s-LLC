using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salaries.Domain.SharedKernel
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
