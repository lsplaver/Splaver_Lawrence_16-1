using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QuarterlySales.Models
{
    public class SalesContext : IdentityDbContext<User>
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        public DbSet<Sales> Sales { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    DateOfBirth = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId = 0 // has no manager
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Katherine",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1966, 8, 26),
                    DateOfHire = new DateTime(1999, 1, 1),
                    ManagerId = 1
                },
                new Employee
                {
                    EmployeeId = 3,
                    FirstName = "Grace",
                    LastName = "Hopper",
                    DateOfBirth = new DateTime(1975, 12, 9),
                    DateOfHire = new DateTime(1999, 1, 1),
                    ManagerId = 1
                },
                new Employee
                {
                    EmployeeId = 4,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1987, 1, 1),
                    DateOfHire = new DateTime(1996, 1, 1),
                    ManagerId = 1
                },
                new Employee
                {
                    EmployeeId = 5,
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(2000, 12, 31),
                    DateOfHire = new DateTime(2020, 1, 1),
                    ManagerId = 4
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
                },
                new Sales
                {
                    SalesId = 5,
                    Quarter = 1,
                    Year = 2001,
                    Amount = 344947,
                    EmployeeId = 1
                },
                new Sales
                {
                    SalesId = 6,
                    Quarter = 1,
                    Year = 2001,
                    Amount = 165447,
                    EmployeeId = 4
                }
            );
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "P@ssw0rd";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
