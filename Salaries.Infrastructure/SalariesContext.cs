using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Salaries.Domain.SharedKernel;

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
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Division { get; set; }
    }

    public class Salary
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }

        [StringLength(10)]
        public string EmployeeCode { get; set; }

        [StringLength(150)]
        public string EmployeeName { get; set; }

        [StringLength(150)]
        public string EmployeeSurname { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(10)]
        public string IdentificationNumber { get; set; }
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
                .UseSqlServer("Server=localhost;Database=MarshallsLLC;Trusted_Connection=True;ConnectRetryCount=0");

            return new SalariesContext(optionsBuilder.Options);
;       }

    }
}

