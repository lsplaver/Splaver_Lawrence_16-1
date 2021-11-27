using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesDTO
    {
        public int SaleId { get; set; }
        public double? Amount { get; set; }
        public int? Quarter { get; set; }
        public int? Year { get; set; }
        public int EmployeeId { get; set; }

        public void Load(Sales sales)
        {
            SaleId = sales.SalesId;
            Amount = sales.Amount;
            Quarter = sales.Quarter;
            Year = sales.Year;
            EmployeeId = sales.EmployeeId;
        }
    }
}
