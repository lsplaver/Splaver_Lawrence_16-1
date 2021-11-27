using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class SalesGridDTO : GridDTO
    {
        public const string DefaultFilter = "all";

        public string Employee { get; set; } = DefaultFilter;
        public string Quarter { get; set; } = DefaultFilter;
        public string Year { get; set; } = DefaultFilter;
    }
}
