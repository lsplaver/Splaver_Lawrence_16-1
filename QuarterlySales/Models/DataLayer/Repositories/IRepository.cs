using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> optiions);

        int Count { get; }

        //T Get(QueryOptions<T> options);

        void Insert(T entity);

        void Save();
    }
}
