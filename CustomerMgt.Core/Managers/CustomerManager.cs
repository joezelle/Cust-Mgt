﻿using CustomerMgt.Core.Exceptions;
using CustomerMgt.Core.Interfaces;
using CustomerMgt.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
           _customerRepository = customerRepository;
        }

        public async Task<CustomerModel> Create(CustomerModel customerModel)
        {

            var result = await _customerRepository.Create(customerModel);

            return result ?? throw new BadRequestException("Request failed");
        }

        public async Task<CustomerModel> Delete(long id)
        {
            if (id <= default(long))
            {
                throw new BadRequestException("Id is invalid");
            }

            var result = await _customerRepository.Get(id);

            return result == null ? throw new NotFoundException("customer not found") : await _customerRepository.Delete(id);
        }


        public async Task<CustomerModel> GetById(long id)
        {
            if (id <= default(long))
            {
                throw new BadRequestException("Id is invalid");
            }

            var result = await _customerRepository.Get(id) ?? throw new NotFoundException("Customer not found");

            if (result.IsDeleted || !result.IsActive)
            {
                throw new BadRequestException("Customer is not active");
            }
                
            return result;
        }

        public async Task<Page<CustomerModel>> GetByPage(int pageNumber, int pageSize)
        {
            return await _customerRepository.Get(pageNumber, pageSize);
        }

        public async Task<List<CustomerModel>> GetList()
        {
            return await _customerRepository.Get().ConfigureAwait(false);
        }

        public async Task<CustomerModel> Update(CustomerModel customerModel)
        {
            var result = await _customerRepository.Update(customerModel);

            return result ?? throw new BadRequestException("Request failed");
        }
    }
}
