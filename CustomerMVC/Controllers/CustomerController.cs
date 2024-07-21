using CustomerMgt.MVC.Models;
using CustomerMgt.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMgt.MVC.Controllers
{

    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILoggerService _loggerService;

        public CustomerController(ICustomerService customerService, ILoggerService loggerService)
        {
            _customerService = customerService;
            _loggerService = loggerService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {

            _loggerService.LogInfo("Get All customers");
            var response = await _customerService.GetAllCustomersAsync(pageNumber, pageSize);
            if (response.RequestSuccessful)
            {
                if (response != null && response.ResponseData != null)
                {
                    ViewBag.TotalCount = response.ResponseData.TotalSize;
                    ViewBag.PageNumber = response.ResponseData.PageNumber;
                    ViewBag.PageSize = response.ResponseData.PageSize;
                    return View(response.ResponseData.Items);
                }
                else
                {
                    return View("Error", new ResponseModel<string>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "500",
                        Message = "No Data was returned"
                    });
                }
                
            }
            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            });
        }

        public async Task<IActionResult> Details(long id)
        {
            _loggerService.LogInfo("Get customer detail");
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (response.RequestSuccessful)
            {
                if(response.ResponseData== null)
                {
                    return NotFound();
                }


                return View(response.ResponseData);
            }
            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel customer)
        {
            _loggerService.LogInfo("Create Customer");
            var response = await _customerService.CreateCustomerAsync(customer);
            if (response.RequestSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, CustomerModel customer)
        {
            _loggerService.LogInfo("Edit customer");
            var response = await _customerService.UpdateCustomerAsync(customer);
            if (response.RequestSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            });
        }

        public async Task<IActionResult> Edit(long id)
        {

            var response = await _customerService.GetCustomerByIdAsync(id);
            if (response.RequestSuccessful)
            {
                if (response.ResponseData == null)
                {
                    return View("Error", new ResponseModel<string>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "404",
                        Message = "Customer not found"
                    });
                };

                return View("Edit", response.ResponseData);
            }
            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            }) ;
        }

        public async Task<IActionResult> Delete(long id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (response.RequestSuccessful)
            {
                if (response.ResponseData == null)
                {
                    return View("Error", new ResponseModel<string>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "404",
                        Message = "Customer not found"
                    });
                };

                return View("Delete", response.ResponseData);
            }
            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            _loggerService.LogInfo("Delete Customer");
            var response = await _customerService.DeleteCustomerAsync(id);
            if (response.RequestSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error", new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = response.Message
            });
        }

        public IActionResult Error()
        {
            var responseModel = new ResponseModel<string>
            {
                RequestSuccessful = false,
                ResponseCode = "500",
                Message = "An unexpected error occurred"
            };
            return View(responseModel);
        }

    }
}