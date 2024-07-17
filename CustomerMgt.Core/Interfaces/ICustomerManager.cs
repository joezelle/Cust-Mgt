using CustomerMgt.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Interfaces
{
    public interface ICustomerManager
    {
        Task<CustomerModel> Create(CustomerModel customerModel);
        Task<CustomerModel> Delete(long id);
        Task<CustomerModel> GetById(long id);
        Task<CustomerModel> Update(CustomerModel customerModel);
        Task<Page<CustomerModel>> GetByPage(int pageNumber, int pageSize);
        Task<List<CustomerModel>> GetList();
    }
}
