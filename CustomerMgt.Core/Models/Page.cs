using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Models
{
    public class Page<T>
    {
        public int PageSize { get; }
        public int PageNumber { get; }
        public int TotalSize { get; }
        public List<T> Items { get; set; }

        public Page(List<T> items, int totalSize, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            TotalSize = totalSize;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }
    }
}
