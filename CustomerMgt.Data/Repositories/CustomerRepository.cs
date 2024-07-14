using CustomerMgt.Core.Interfaces;
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
    public class CustomerRepository : ICustomerRepository
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

        public async Task<CustomerModel> Update(CustomerModel model)
        {
            var entity = await _dbContext.Set<Customer>().FindAsync(model.Id);
            if (entity == null)
                return null;

            entity.Address = model.Address;
            entity.Gender = model.Gender;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;


            await _dbContext.SaveChangesAsync();

            return entity.Map();
        }

        public async Task<CustomerModel> Delete(long Id)
        {
            var entity = await _dbContext.Set<Customer>().FindAsync(Id);
            if (entity == null)
            {
                return null;
            }

            if (!entity.IsDeleted)
            {
                entity.IsActive = false;
                entity.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
                

            return entity.Map();
        }

        public async Task<CustomerModel> Get(long id)
        {
            var entity = await _dbContext.Set<Customer>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            return entity.Map();
        }

    }
}
