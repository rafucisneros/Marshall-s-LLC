using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Salaries.Domain.AggregatesModel.DivisionAggregate;
using Salaries.Domain.AggregatesModel.OfficeAggregate;
using Salaries.Domain.AggregatesModel.PositionAggregate;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
using Salaries.Domain.SharedKernel;

namespace Salaries.Infrastructure
{
    public class SalariesContext: DbContext, IUnitOfWork
    {
        public SalariesContext(DbContextOptions<SalariesContext> options) : base(options)
        {

        }

        public SalariesContext() : base()
        {

        }

        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    public class SalariesContextDesignFactory : IDesignTimeDbContextFactory<SalariesContext>
    {
        public SalariesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalariesContext>()
                .UseSqlServer("Server=localhost;Database=MarshallsLLC;Trusted_Connection=True;ConnectRetryCount=0");

            return new SalariesContext(optionsBuilder.Options);
;       }

    }
}

