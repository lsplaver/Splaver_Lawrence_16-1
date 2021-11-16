using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesListViewModel
    {
        public List<Employee> Employees { get; set; }

        public List<Sales> Sales { get; set; }

        public int EmployeeId { get; set; }
    }
}
