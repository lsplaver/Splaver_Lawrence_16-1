using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesListViewModel
    {
        public IEnumerable<Sales> Sales { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public List<Sales> Years { get; set; }
        public IEnumerable<int> SalesListYear
        {
            get
            {
                List<int> years = new List<int>();

                int foundingYear = 1995;

                int maxYear = DateTime.Now.Year;

                for (int year = maxYear; year >= foundingYear; year--)
                {
                    years.Add(year);
                }

                return years;
            }
        }

        public int[] SalesListQuarter { get; set; }
    }
}
