using CustomerMgt.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Data.Utilities
{
    public static class Extensions
    {
        public static async Task<Page<T>> ToPageListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            var items = await query.Skip(offset).Take(pageSize).ToListAsync();
            return new Page<T>(items, count, pageNumber, pageSize);
        }
    }
}
