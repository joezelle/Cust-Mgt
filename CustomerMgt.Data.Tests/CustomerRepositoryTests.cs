using CustomerMgt.Core.Models;
using CustomerMgt.Data.Entities;
using CustomerMgt.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CustomerMgt.Data.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly CustomerRepository _repositoryTests;
        private readonly APPContext _appContext;

        public CustomerRepositoryTests() 
        {
            var options = new DbContextOptionsBuilder<APPContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;

            _appContext = new APPContext(options);
            _repositoryTests = new CustomerRepository(_appContext);
        
        }


        private CustomerModel GetMockCustomer()
        {
            return new CustomerModel
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = "Male",
                Address = "123 Main St",
                Email = "john.doe@example.com"
            };
        }

        [Fact]
        public async Task AddCustomer_ValidCustomer_SavesToDatabase()
        {

            var mockCustomer = GetMockCustomer();

            var customer = await _repositoryTests.Create(mockCustomer);
            customer.FirstName.Should().Be("John");
            customer.Should().NotBeNull();

            var savedCustomer = await _appContext.Customers.FindAsync(customer.Id);
            savedCustomer.Should().NotBeNull();
            savedCustomer?.FirstName.Should().Be("John");
            savedCustomer?.LastName.Should().Be("Doe");
            
        }
    }
}