using CustomerMgt.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> Get(long id);
        Task<CustomerModel> Update(CustomerModel model);
        Task<CustomerModel> Create(CustomerModel model);
        Task<CustomerModel> Delete(long id);
    }
}
