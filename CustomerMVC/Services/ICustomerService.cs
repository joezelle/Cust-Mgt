using CustomerMgt.MVC.Models;

namespace CustomerMgt.MVC.Services
{
    public interface ICustomerService
    {
        Task<ResponseModel<Page<CustomerModel>>> GetAllCustomersAsync(int pageNumber, int pageSize);
        Task<ResponseModel<CustomerModel>> GetCustomerByIdAsync(long id);
        Task<ResponseModel<CustomerModel>> CreateCustomerAsync(CustomerModel customer);
        Task<ResponseModel<CustomerModel>> UpdateCustomerAsync( CustomerModel customer);
        Task<ResponseModel<CustomerModel>> DeleteCustomerAsync(long id);
    }
}
