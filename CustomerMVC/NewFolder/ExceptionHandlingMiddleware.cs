
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace YourNamespace.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (context.Request.Headers["Accept"].ToString().Contains("text/html"))
            {
                context.Response.Redirect("/Customer/Error");
            }
            else
            {
                var response = new
                {
                    context.Response.StatusCode,
                    Message = "An error occurred while processing your request.",
                    Detailed = exception.Message
                };

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        }
    }
}
