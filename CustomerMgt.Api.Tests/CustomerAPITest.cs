using CustomerMgt.API.Controllers;
using CustomerMgt.Core.Interfaces;
using CustomerMgt.Core.Models;
using CustomerMgt.Core.RequestModels;
using CustomerMgt.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CustomerMgt.Api.Tests
{
    public class CustomerAPITest
    {
        private readonly Mock<ICustomerManager> _mockManager;
        private readonly CustomerController _controller;

        public CustomerAPITest()
        {
            _mockManager =  new Mock<ICustomerManager>();
            _controller = new CustomerController(_mockManager.Object);
        }

        private CustomerModel GetMockCustomer()
        {
            return new CustomerModel
            {
                Id = 1,
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
            Assert.Equal(expectedCustomer.Id, actualCustomer.Id);
            Assert.Equal(expectedCustomer.FirstName, actualCustomer.FirstName);
            Assert.Equal(expectedCustomer.LastName, actualCustomer.LastName);
            Assert.Equal(expectedCustomer.Email, actualCustomer.Email);
            Assert.Equal(expectedCustomer.Gender, actualCustomer.Gender);
            Assert.Equal(expectedCustomer.Address, actualCustomer.Address);
        }

        [Fact]
        public async Task GetCustomerById_ValidId_ReturnsCustomerDetails()
        {
            
            var customerId = 1;
            var expectedCustomer = GetMockCustomer();
            _mockManager.Setup(manager => manager.GetById(customerId))
                        .ReturnsAsync(expectedCustomer);

            
            var result = await _controller.GetCustomerById(customerId);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var responseModel = okResult.Value as ResponseModel<object>;
            Assert.NotNull(responseModel);
            Assert.True(responseModel.RequestSuccessful);
            Assert.Equal(ResponseCodes.Successful, responseModel.ResponseCode);

            var returnedCustomer = responseModel.ResponseData as CustomerModel;


            Assert.NotNull(returnedCustomer);
            AssertCustomerProperties(returnedCustomer, expectedCustomer);
        }

        [Fact]
        public async Task GetCustomerById_InvalidId_ReturnsNotFound()
        {
            var invalidCustomerId = 999;
            _mockManager.Setup(manager => manager.GetById(invalidCustomerId))
                        .ReturnsAsync((CustomerModel)null);

            // Act
            var result = await _controller.GetCustomerById(invalidCustomerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel<object>>(okResult.Value);
            Assert.NotNull(responseModel);
            Assert.True(responseModel.RequestSuccessful);
            Assert.Equal(ResponseCodes.Successful, responseModel.ResponseCode);
        }

        [Fact]
        public async Task UpdateCustomer_ValidId_ReturnsUpdatedCustomer()
        {
            // Arrange
            var existingCustomer = GetMockCustomer();
            var updatedCustomer = GetMockCustomer();
            updatedCustomer.FirstName = "Jane";
            updatedCustomer.LastName = "Smith";
            updatedCustomer.Gender = "Female";
            updatedCustomer.Address = "456 Main St";
            updatedCustomer.Email = "jane.smith@email.com";

            var requestModel = new CustomerRequestModel
            {
                FirstName = updatedCustomer.FirstName,
                LastName = updatedCustomer.LastName,
                Gender = updatedCustomer.Gender,
                Address = updatedCustomer.Address,
                Email = updatedCustomer.Email
            };


            _mockManager.Setup(manager => manager.GetById(existingCustomer.Id))
                        .ReturnsAsync(existingCustomer);
            _mockManager.Setup(manager => manager.Update(It.IsAny<CustomerModel>()))
            .ReturnsAsync(updatedCustomer);

            // Act
            var result = await _controller.Update(requestModel, updatedCustomer.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel<object>>(okResult.Value);
            Assert.NotNull(responseModel);
            Assert.True(responseModel.RequestSuccessful);
            Assert.Equal(ResponseCodes.Successful, responseModel.ResponseCode);


            var returnedCustomer = responseModel.ResponseData as CustomerModel;
            Assert.IsType<CustomerModel>(returnedCustomer);
            AssertCustomerProperties(returnedCustomer, updatedCustomer);


        }

        [Fact]
        public async Task CreateCustomer_ValidData_ReturnsCreatedCustomer()
        {
            // Arrange
            var newCustomer = GetMockCustomer();
            var requestModel = new CustomerRequestModel
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Gender = newCustomer.Gender,
                Address = newCustomer.Address,
                Email = newCustomer.Email
            };

            _mockManager.Setup(manager => manager.Create(It.IsAny<CustomerModel>()))
                        .ReturnsAsync(newCustomer);

            // Act
            var result = await _controller.CreateCustomer(requestModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel<object>>(okResult.Value);
            Assert.NotNull(responseModel);
            Assert.True(responseModel.RequestSuccessful);
            Assert.Equal(ResponseCodes.Successful, responseModel.ResponseCode);


            var createdCustomer = responseModel.ResponseData as CustomerModel;
            Assert.IsType<CustomerModel>(createdCustomer);
            AssertCustomerProperties(newCustomer, createdCustomer );
        }

        [Fact]
        public async Task DeleteCustomer_ValidId_ReturnsOk()
        {
            // Arrange
            var existingCustomer = GetMockCustomer();
            var customerId = 12;
            
            var deletedCustomer = new CustomerModel
            {
                Id = customerId,
                IsDeleted = true,
                IsActive = false
            };

            _mockManager.Setup(manager => manager.GetById(existingCustomer.Id))
                        .ReturnsAsync(existingCustomer);
            _mockManager.Setup(manager => manager.Delete(deletedCustomer.Id))
                        .ReturnsAsync(deletedCustomer);

            // Act
            var result = await _controller.Delete(existingCustomer.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel<object>>(okResult.Value);
            Assert.NotNull(responseModel);
            Assert.True(responseModel.RequestSuccessful);
            Assert.Equal(ResponseCodes.Successful, responseModel.ResponseCode);

            
        }

    }
}