using CustomerMgt.Core.Interfaces;
using CustomerMgt.Core.Managers;
using CustomerMgt.Core.Models;
using Moq;

namespace CustomerMgt.Core.Tests
{
    public class CustomerServiceTests
    {

        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerManager _customerManager;
        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _customerManager = new CustomerManager(_mockRepository.Object);
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

        private void AssertCustomerProperties(CustomerModel actualCustomer, CustomerModel expectedCustomer)
        {
            Assert.NotNull(actualCustomer);
            Assert.True(actualCustomer.Id == expectedCustomer.Id);
            Assert.True(actualCustomer.FirstName == expectedCustomer.FirstName);
            Assert.True(actualCustomer.LastName == expectedCustomer.LastName);
            Assert.True(actualCustomer.Email == expectedCustomer.Email);
            Assert.True(actualCustomer.Equals(expectedCustomer));
        }


        [Fact]
        public async Task CreateNewCustomer_ValidData_ReturnsCreatedCustomer()
        {

            var newCustomer = GetMockCustomer(); 
            _mockRepository.Setup(repo => repo.Create(It.IsAny<CustomerModel>()))
                          .ReturnsAsync(newCustomer);

            var createdCustomer = await _customerManager.Create(newCustomer);

            Assert.NotNull(createdCustomer);
            AssertCustomerProperties(newCustomer, createdCustomer);

        }

        [Fact]
        public async Task DeleteCustomer_ValidId_ShouldDeleteSuccessfully()
        {
            var customerId = 12;
            var deletedCustomer =  new CustomerModel
            {
                Id = customerId,
                IsDeleted = true,
                IsActive = false,
                
            };

            _mockRepository.Setup(repo => repo.Delete(customerId))
                          .ReturnsAsync(deletedCustomer);

            var result = await _customerManager.Delete(customerId);

            //Assert.NotNull(result);
            //Assert.True(result.IsDeleted);
            //Assert.False(result.IsActive);


        }

        [Fact]
        public async Task GetCustomerById_ValidId_ReturnsCustomer()
        {
            var customerId = 12;
            var expectedCustomer = GetMockCustomer();

            _mockRepository.Setup(repo => repo.Get(customerId))
                           .ReturnsAsync(expectedCustomer);

            var result = await _customerManager.GetById(customerId);

            Assert.NotNull(result);
            AssertCustomerProperties(result, expectedCustomer);
        }

        [Fact]
        public async Task UpdateCutomerById_ValidData_ReturnsUpdatedCustomer()
        {
            var existingCustomer = GetMockCustomer();



            _mockRepository.Setup(repo => repo.Update(It.IsAny<CustomerModel>()))
                           .ReturnsAsync(existingCustomer);

            var updatedCustomer = await _customerManager.Update(existingCustomer);

            AssertCustomerProperties(updatedCustomer, existingCustomer);
        }
    }
}