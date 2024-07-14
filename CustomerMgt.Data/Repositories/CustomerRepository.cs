using CustomerMgt.Core.Models;
using CustomerMgt.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Data.Repositories
{
    public class CustomerRepository
    {
        private DbContext _dbContext;

        public CustomerRepository(DbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerModel> Create(CustomerModel model)
        {

            Customer customer = model.Map();
            _dbContext.Set<Customer>().Add(customer);

            await _dbContext.SaveChangesAsync();
            return customer.Map();

        }
    }
}
