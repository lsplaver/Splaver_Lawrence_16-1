using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class QuarterlySalesUnitOfWork : IQuarterlySalesUnitOfWork
    {
        private SalesContext context { get; set; }
        public QuarterlySalesUnitOfWork(SalesContext ctx) => context = ctx;

        private Repository<Employee> empployeeData;
        public Repository<Employee> Employees
        {
            get
            {
                if (empployeeData == null)
                {
                    empployeeData = new Repository<Employee>(context);
                }
                return empployeeData;
            }
        }

        private Repository<Sales> salesData;
        public Repository<Sales> Sales
        {
            get
            {
                if (salesData == null)
                {
                    salesData = new Repository<Sales>(context);
                }
                return salesData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
