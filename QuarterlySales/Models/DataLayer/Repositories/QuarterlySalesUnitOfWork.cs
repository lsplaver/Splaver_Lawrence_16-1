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

        private IRepository<Employee> empployeeData;
        public IRepository<Employee> Employees
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

        private IRepository<Sales> salesData;
        public IRepository<Sales> Sales
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
