using CustomerMgt.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
            base.Code = ResponseCodes.NotFound;
            base.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
