using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesGridBuilder : GridBuilder
    {
        public SalesGridBuilder(SalesGridDTO values, string defaultSortField) : base(values, defaultSortField)
        {
            bool isInitial = values.Employee.IndexOf(RouteDictionary.Employee) == -1;
            if (isInitial)
            {
                routes.EmployeeFilter = RouteDictionary.Employee + values.Employee;
                routes.QuarterFilter = RouteDictionary.Quarter + values.Quarter;
                routes.YearFilter = RouteDictionary.Year + values.Year;
            }
            else
            {
                routes.EmployeeFilter = values.Employee;
                routes.QuarterFilter = values.Quarter;
                routes.YearFilter = values.Year;
            }
        }

        public void LoadFilterSegments(string[] filter, Employee employee)
        {
            if (employee == null)
            {
                routes.EmployeeFilter = RouteDictionary.Employee + filter[0];
            }
            else
            {
                routes.EmployeeFilter = RouteDictionary.Employee + filter[0] + "-" + employee.FullName.Slug();
            }

            routes.QuarterFilter = RouteDictionary.Quarter + filter[1];
            routes.YearFilter = RouteDictionary.Year + filter[2];
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string def = SalesGridDTO.DefaultFilter;
        public bool IsFilterByEmployee => routes.EmployeeFilter != def;
        public bool IsFilterByQuarter => routes.QuarterFilter != def;
        public bool IsFilterByYear => routes.YearFilter != def;

        public bool IsSortByQuarter => routes.SortField.EqualsNoCase(nameof(Sales.Quarter));
        public bool ISSortByYear => routes.SortField.EqualsNoCase(nameof(Sales.Year));
    }
}
