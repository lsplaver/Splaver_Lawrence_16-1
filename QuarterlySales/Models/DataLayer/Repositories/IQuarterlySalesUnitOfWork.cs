using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public interface IQuarterlySalesUnitOfWork
    {
        IRepository<Employee> Employees { get; }

        IRepository<Sales> Sales { get; }

        void Save();
    }
}
