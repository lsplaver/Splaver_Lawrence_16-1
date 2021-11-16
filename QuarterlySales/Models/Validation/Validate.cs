using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models.Validation
{
    public static class Validate
    {
        public static string CheckEmployee(SalesContext context, Employee employee)
        {
            Employee searchEmployee = context.Employees.FirstOrDefault(
                e => e.FirstName == employee.FirstName
                && e.LastName == employee.LastName
                && e.DateOfBirth == employee.DateOfBirth
            );

            return searchEmployee == null ?
                string.Empty
                : $"{searchEmployee.FullName} (DOB: {searchEmployee.DateOfBirth?.ToShortDateString()}) is already in the database.";
        }

        public static string CheckManagerEmployeeMatch(SalesContext context, Employee employee)
        {
            Employee manager = context.Employees.Find(employee.ManagerId);

            if (manager != null
                && manager.FirstName == employee.FirstName
                && manager.LastName == employee.LastName
                && manager.DateOfBirth == employee.DateOfBirth)
            {
                return $"Manager and employee can't be the same person.";
            }

            return string.Empty;
        }

        public static string CheckSales(SalesContext context, Sales sales)
        {
            throw new NotImplementedException();
        }
    }
}
