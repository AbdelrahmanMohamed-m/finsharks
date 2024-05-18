using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utils
{
    public class QueryObject
    {
        public String? Symbol { set; get; } = null;
        public String? CompanyName { set; get; } = null;
        public String? SortBy { get; set; } = "Symbol";
        public bool IsDesending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}