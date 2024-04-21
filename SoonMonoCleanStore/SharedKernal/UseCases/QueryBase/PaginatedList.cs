using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.QueryBase
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; private set; }
        //public int TotalCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedList(IEnumerable<T> items, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }
    }

}
