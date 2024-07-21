using CustomerMgt.Core.Interfaces;
using CustomerMgt.Core.Models;
using CustomerMgt.Data.Entities;
using CustomerMgt.Data.Utilities;
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
        private readonly DbContext _dbContext;

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
            entity.PhoneNumber = model.PhoneNumber;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Id = model.Id;


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

        public async Task<Page<CustomerModel>> Get(int pageNumber, int pageSize)
        {
            var query = _dbContext.Set<Customer>()
                .Select(result => new CustomerModel
                {
                    DateCreated = result.DateCreated,
                    IsActive = result.IsActive,
                    IsDeleted = result.IsDeleted,
                    Id = result.Id,
                    Address = result.Address,
                    Email = result.Email,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    PhoneNumber= result.PhoneNumber
                })
                .OrderByDescending(x => x.DateCreated).Where(x => !x.IsDeleted);

            var results = await query.ToPageListAsync(pageNumber, pageSize);

            return results;
        }

        public async Task<List<CustomerModel>> Get()
        {
            var query = _dbContext.Set<Customer>().Select(result => new CustomerModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Address = result.Address,
                DateCreated = result.DateCreated,
                IsActive = result.IsActive,
                IsDeleted = result.IsDeleted,
                Id = result.Id,
                PhoneNumber = result.PhoneNumber
                

            }).OrderByDescending(x => x.LastName);

            var results = await query.ToListAsync();
            return results;
        }
    }
}
