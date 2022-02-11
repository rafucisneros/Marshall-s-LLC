using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Salaries.Infrastructure
{
    public class SalariesContext: DbContext
    {
        public SalariesContext(DbContextOptions<SalariesContext> options) : base(options)
        {

        }

        public DbSet<Office> Offices { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }

    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Salary
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public string EmployeeCode { get; set; } // string 10
        public string EmployeeName { get; set; } // string 150
        public string EmployeeSurname { get; set; } // string 150
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }
        public string IdentificationNumber { get; set; } // string 10
        public decimal BaseSalary { get; set; }
        public decimal CompensationBonus { get; set; }
        public decimal ProductionBonus { get; set; }
        public decimal Commission { get; set; }
        public decimal Contributions { get; set; }
    }

    public class SalariesContextDesignFactory : IDesignTimeDbContextFactory<SalariesContext>
    {
        public SalariesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalariesContext>()
                .UseSqlServer("Server=localhost;Database=MarshallsLLC;Trusted_Connection=True;ConnectRetryCount=0"); // Esto deberia ser una variable de configuracion en appconfig

            return new SalariesContext(optionsBuilder.Options);
;       }

    }
}

