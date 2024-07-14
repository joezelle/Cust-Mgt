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


        [Fact]
        public async Task AddCustomer_ValidCustomer_SavesToDatabase()
        {
            

            var mockCustomer = new Mock<CustomerModel>();


            await _repositoryTests.Create(mockCustomer.Object);


            var savedCustomer = _appContext.Customers.FindAsync(mockCustomer.Object.Id);
            savedCustomer.Should().NotBeNull();
        }
    }
}