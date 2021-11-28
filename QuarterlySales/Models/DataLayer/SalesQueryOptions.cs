using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesQueryOptions : QueryOptions<Sales>
    {
        public void SortFilter(SalesGridBuilder builder)
        {
            if (builder.IsFilterByQuarter)
            {
                Where = s => s.Quarter.ToString() == builder.CurrentRoute.QuarterFilter;
            }

            if (builder.IsFilterByYear)
            {
                Where = s => s.Year.ToString() == builder.CurrentRoute.YearFilter;
            }

            if (builder.IsFilterByEmployee)
            {
                int id = builder.CurrentRoute.EmployeeFilter.ToInt();
                if (id > 0)
                {
                    Where = s => s.Employee.EmployeeId == id;
                }
            }

            if (builder.IsSortByQuarter)
            {
                OrderBy = s => s.Quarter;
            }
            else if (builder.IsFilterByYear)
            {
                OrderBy = s => s.Year;
            }
            else
            {
                OrderBy = s => s.Employee.LastName;
            }
        }
    }
}
