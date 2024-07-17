using CustomerMgt.Core.Exceptions;
using CustomerMgt.Core.Interfaces;
using CustomerMgt.Core.Models;
using CustomerMgt.Core.RequestModels;
using CustomerMgt.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerMangager;

        public CustomerController(ICustomerManager customerMangager)
        {
            _customerMangager = customerMangager;
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetCustomerById(long id)
        {


            //_loggerService.LogInfo("Get Employee");

            if (id <= default(long))
            {
                throw new BadRequestException("Customer Id is required");
            }
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = await _customerMangager.GetById(id),
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequestModel model)
        {
            

            //model.Validate();
            var result = await _customerMangager.Create(new CustomerModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                DateCreated = DateTime.Now,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,



            });

            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result,
                Message = "Customer has been registered successfully"
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CustomerRequestModel model, long id)
        {
            if (id <= default(long))
            {
                throw new BadRequestException("Customer Id is required");
            }

            var result = await _customerMangager.Update(new CustomerModel
            {

                Id = id,
                Address = model.Address,
                Email = model.Email,
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                LastName = model.LastName,

            });

            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                Message = "Customer has been successfully updated",
                ResponseData = result
            }); ;
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= default(long))
            {
                throw new BadRequestException("Customer Id is required");
            }

            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = await _customerMangager.Delete(id)
            });
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 20)
        {
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                Message = "Successful",
                ResponseData = await _customerMangager.GetByPage(pageNumber, pageSize)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                Message = "Successful",
                ResponseData = await _customerMangager.GetList()
            });
        }
    }
}
