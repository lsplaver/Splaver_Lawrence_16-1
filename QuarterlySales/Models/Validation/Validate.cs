using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models.Validation
{
    public static class Validate
    {
        public static string CheckEmployee(Repository<Employee> data, Employee employee)
        {
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

        public static string CheckManagerEmployeeMatch(Repository<Employee> data, Employee employee)
        {
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

        public static string CheckSales(IQuarterlySalesUnitOfWork data, Sales sale)
        {
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

            Employee employee = data.Employees.Get(sale.EmployeeId);
            return $"Sales for {employee.FullName} for {sale.Year} Q{sale.Quarter} are already in the database.";
        }

        public static string CheckSalesYear(Repository<Employee> data, Sales sale)
        {
            Employee employee = data.Get(sale.EmployeeId);
            int hireYear = employee.DateOfHire.Value.Year;

            if (sale.Year >= hireYear)
            {
                return string.Empty;
            }

                return $"Sales for {employee.FullName} for {sale.Year} year that the employee was hired.";
        }
        public static string CheckSalesQuarter(Repository<Employee> data, Sales sale)
        {
            Employee employee = data.Get(sale.EmployeeId);
            int hireMonth = employee.DateOfHire.Value.Month;
            int hireQuarter = 0;
            if (hireMonth == 1 || hireMonth == 2 || hireMonth == 3)
            {
                hireQuarter = 1;
            }
            else if (hireMonth == 4 || hireMonth == 5 || hireMonth == 6)
            {
                hireQuarter = 2;
            }
            else if (hireMonth == 7 || hireMonth == 8 || hireMonth == 9)
            {
                hireQuarter = 3;
            }
            else
            {
                hireQuarter = 4;
            }

            if (sale.Quarter >= hireQuarter)
            {
                return string.Empty;
            }

            return $"Sales for {employee.FullName} for  Q{sale.Quarter} is before the quarter of the year that the employee was hired.";
        }
    }
}
