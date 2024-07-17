using CustomerMgt.MVC.Models;

namespace CustomerMgt.MVC
{
    public class CustomerResponse
    {
        public bool RequestSuccessful { get; set; }
        public IEnumerable<Customer>? Customers { get; set; }
    }
}
