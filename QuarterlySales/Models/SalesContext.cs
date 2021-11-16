using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuarterlySales.Models
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        public DbSet<Sales> Sales { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EnployeeId = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    DateOfBirth = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId = 0 // has no manager
                },
                new Employee
                {
                    EnployeeId = 2,
                    FirstName = "Katherine",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1966, 8, 26),
                    DateOfHire = new DateTime(1999, 1, 1),
                    ManagerId = 1
                },
                new Employee
                {
                    EnployeeId = 3,
                    FirstName = "Grace",
                    LastName = "Hopper",
                    DateOfBirth = new DateTime(1975, 12, 9),
                    DateOfHire = new DateTime(1999, 1, 1),
                    ManagerId = 1
                }
            );

            modelBuilder.Entity<Sales>().HasData(
                new Sales
                {
                    SalesId = 1,
                    Quarter = 4,
                    Year = 2019,
                    Amount = 23456,
                    EmployeeId = 2
                },
                new Sales
                {
                    SalesId = 2,
                    Quarter = 1,
                    Year = 2020,
                    Amount = 34567,
                    EmployeeId = 2
                },
                new Sales
                {
                    SalesId = 3,
                    Quarter = 4,
                    Year = 2019,
                    Amount = 19876,
                    EmployeeId = 3
                },
                new Sales
                {
                    SalesId = 4,
                    Quarter = 1,
                    Year = 2020,
                    Amount = 31009,
                    EmployeeId = 3
                }
            );
        }
    }
}
