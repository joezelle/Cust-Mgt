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


        private static CustomerModel GetMockCustomer()
        {
            return new CustomerModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "000111222333",
                Address = "123 Main St",
                Email = "john.doe@example.com"
            };
        }

        private async Task<CustomerModel> CreateAndSaveCustomerAsync(CustomerModel customer)
        {
            var createdCustomer = await _repositoryTests.Create(customer);
            await _appContext.SaveChangesAsync();
            return createdCustomer;
        }

       

        [Fact]
        public async Task AddCustomer_ValidCustomer_SavesToDatabase()
        {

            var mockCustomer = GetMockCustomer();

            var newCustomer = await _repositoryTests.Create(mockCustomer);
            newCustomer.FirstName.Should().Be("John");
            newCustomer.Should().NotBeNull();

            var savedCustomer = await _appContext.Customers.FindAsync(newCustomer.Id);
            savedCustomer.Should().NotBeNull();
            savedCustomer?.FirstName.Should().Be("John");
            savedCustomer?.LastName.Should().Be("Doe");
            
        }

        [Fact]
        public async Task EditCustomer_ValidCustomer_SavesToDatabase()
        {
            var mockCustomer = GetMockCustomer();
            var newCustomer = await CreateAndSaveCustomerAsync(mockCustomer);

            // Modify the customer details
            newCustomer.FirstName = "Jane";
            newCustomer.LastName = "Smith";
            newCustomer.PhoneNumber = "00011122233";
            newCustomer.Address = "456 Main St";
            newCustomer.Email = "jane.smith@email.com";


            var updatedCustomer = await _repositoryTests.Update(newCustomer);

            updatedCustomer.FirstName.Should().Be("Jane");
            updatedCustomer.Should().NotBeNull();


            var savedCustomer = await _appContext.Customers.FindAsync(updatedCustomer.Id);

            savedCustomer.Should().NotBeNull();
            savedCustomer?.FirstName.Should().Be("Jane");
            savedCustomer?.LastName.Should().Be("Smith");
            savedCustomer?.Email.Should().Be("jane.smith@email.com");
        }


        [Fact]
        public async Task DeleteCustomer_ById_SavesToDatabase()
        {
            var mockCustomer = GetMockCustomer();
            var newcustomer = await CreateAndSaveCustomerAsync(mockCustomer);


            var deletedCustomer = await _repositoryTests.Delete(newcustomer.Id);

            deletedCustomer.Should().NotBeNull();
            deletedCustomer.IsDeleted.Should().BeTrue();
            deletedCustomer.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task GetCustomer_ById_ReturnsCustomer()
        {
            var mockCustomer = GetMockCustomer();
            var newCustomer = await CreateAndSaveCustomerAsync(mockCustomer);

            var retrievedCustomer = await _repositoryTests.Get(newCustomer.Id);
            retrievedCustomer.Should().NotBeNull();
            retrievedCustomer.Id.Should().Be(newCustomer.Id);


        }
    }
}