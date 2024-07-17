using CustomerMgt.MVC.Models;
using CustomerMgt.MVC.Services;
using CustomerMgt.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMgt.MVC.Controllers
{
    
        public class CustomerController : Controller
        {
            private readonly CustomerService _customerService;

            public CustomerController(CustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
            {
                var customers = await _customerService.GetCustomersAsync(pageNumber,pageSize);
                return View(customers);
            }

            public IActionResult Create()
            {
                return View();
            }

            //[HttpPost]
            //public async Task<IActionResult> Create(Customer customer)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        await _customerService.CreateCustomerAsync(customer);
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View(customer);
            //}

            public async Task<IActionResult> Edit(int id)
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }

            //[HttpPost]
            //public async Task<IActionResult> Edit(Customer customer)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        await _customerService.UpdateCustomerAsync(customer);
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View(customer);
            //}

            public async Task<IActionResult> Delete(int id)
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }

            //[HttpPost, ActionName("Delete")]
            //public async Task<IActionResult> DeleteConfirmed(int id)
            //{
            //    await _customerService.DeleteCustomerAsync(id);
            //    return RedirectToAction(nameof(Index));
            //}

            
        }
    
}
