using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesGridBuilder : GridBuilder
    {
        public SalesGridBuilder(ISession sess) : base(sess) { }
        public SalesGridBuilder(ISession sess, SalesGridDTO values, string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Employee.IndexOf(RouteDictionary.Employee) == -1;
            routes.EmployeeFilter = (isInitial) ? RouteDictionary.Employee + values.Employee : values.Employee;
            routes.QuarterFilter = (isInitial) ? RouteDictionary.Quarter + values.Quarter : values.Quarter;
            routes.YearFilter = (isInitial) ? RouteDictionary.Year + values.Year : values.Year;
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
        public bool IsSortByYear => routes.SortField.EqualsNoCase(nameof(Sales.Year));
        public bool IsSortByAmount => routes.SortField.EqualsNoCase(nameof(Sales.Amount));
    }
}
