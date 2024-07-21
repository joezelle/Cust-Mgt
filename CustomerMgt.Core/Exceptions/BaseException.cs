using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Exceptions
{
    public class BaseException : Exception
    {
        public string Code { get; set; } = "";
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public BaseException(string message) : base(message)
        {

        }

        //public BaseException(string code, string message, Exception exception) : base(message, exception)
        //{
            
        //}
    }
}
