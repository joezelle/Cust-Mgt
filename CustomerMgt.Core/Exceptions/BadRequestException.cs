using CustomerMgt.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Exceptions
{
    public class BadRequestException :BaseException
    {
        public BadRequestException(string message) : base(message)
        {
            base.Code = ResponseCodes.Failed;
            base.httpStatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}
