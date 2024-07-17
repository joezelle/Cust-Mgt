using CustomerMgt.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Data
{
    public class APPContext : DbContext
    {
        public APPContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
