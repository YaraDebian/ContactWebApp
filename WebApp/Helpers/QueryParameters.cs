using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public class QueryParameters
    {
        const int maxPagesize = 50;
        public int Pagenumber { get; set; } = 1;

        private int _pagesize = 100;
        public int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = (value > maxPagesize) ? maxPagesize : value;
            }
        }
    }
}
