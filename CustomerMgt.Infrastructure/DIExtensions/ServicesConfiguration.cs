using CustomerMgt.Core.Interfaces;
using CustomerMgt.Core.Managers;
using CustomerMgt.Core.Services;
using CustomerMgt.Data.Repositories;
using CustomerMgt.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Infrastructure.DIExtensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            //Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();


            //Manager
            services.AddScoped<ICustomerManager, CustomerManager>();

            //Services
            services.AddScoped<ILoggerService, LoggerService>();
        }
    }
}
