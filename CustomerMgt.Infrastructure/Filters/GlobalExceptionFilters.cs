using CustomerMgt.Core.Exceptions;
using CustomerMgt.Core.Models;
using CustomerMgt.Core.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Infrastructure.Filters
{
    public class GlobalExceptionFilter
    {
        public static (ResponseModel<T> responseModel, HttpStatusCode statusCode) GetStatusCode<T>(Exception exception)
        {
            return exception switch
            {
                BaseException bex => (new ResponseModel<T>
                {
                    ResponseCode = bex.Code,
                    Message = bex.Message,
                    RequestSuccessful = false,
                }, bex.HttpStatusCode),
                ValidationException bex => (new ResponseModel<T>
                {
                    ResponseCode = ResponseCodes.ModelValidation,
                    Message = bex.Message,
                    RequestSuccessful = false,
                }, HttpStatusCode.BadRequest),
                _ => (new ResponseModel<T>
                {
                    ResponseCode = ResponseCodes.Failed,
                    Message = exception.Message,
                    RequestSuccessful = false
                }, HttpStatusCode.InternalServerError),
            };
        }
    }
}
