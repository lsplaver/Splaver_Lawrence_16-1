using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models.Validation
{
    public static class Validate
    {
        public static string CheckEmployee(/*SalesContext context*/ Repository<Employee> data, Employee employee)
        {
            //Employee searchEmployee = context.Employees.FirstOrDefault(
            //    e => e.FirstName == employee.FirstName
            //    && e.LastName == employee.LastName
            //    && e.DateOfBirth == employee.DateOfBirth
            //);

            var options = new QueryOptions<Employee>
            {
                Where = e => e.FirstName == employee.FirstName
                && e.LastName == employee.LastName
                && e.DateOfBirth == employee.DateOfBirth
            };

            Employee searchEmployee = data.Get(options);

            return searchEmployee == null ?
                string.Empty
                : $"{searchEmployee.FullName} (DOB: {searchEmployee.DateOfBirth?.ToShortDateString()}) is already in the database.";
        }

        public static string CheckManagerEmployeeMatch(/*SalesContext context*/ Repository<Employee> data, Employee employee)
        {
            // Employee manager = context.Employees.Find(employee.ManagerId);
            Employee manager = data.Get(employee.ManagerId);

            if (manager != null
                && manager.FirstName == employee.FirstName
                && manager.LastName == employee.LastName
                && manager.DateOfBirth == employee.DateOfBirth)
            {
                return $"Manager and employee can't be the same person.";
            }

            return string.Empty;
        }

        public static string CheckSales(/*SalesContext context*/ IQuarterlySalesUnitOfWork data, Sales sale)
        {
            //Sales sales = context.Sales.FirstOrDefault(
            var options = new QueryOptions<Sales>
            {
                Where = s => s.EmployeeId == sale.EmployeeId
                && s.Year == sale.Year
                && s.Quarter == sale.Quarter
            };

            Sales sales = data.Sales.Get(options);

            if (sales == null)
            {
                return string.Empty;
            }

            // Employee employee = context.Employees.Find(sale.EmployeeId);
            Employee employee = data.Employees.Get(sale.EmployeeId);
            return $"Sales for {employee.FullName} for {sale.Year} Q{sale.Quarter} are already in the database.";
        }
    }
}
