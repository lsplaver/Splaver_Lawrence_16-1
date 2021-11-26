using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySalesUpdated.Models
{
    public static  class QueryExtensions
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> item, int pagenumber, int pagesize)
        {
            return item
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize);
        }
    }
}
