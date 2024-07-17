using CustomerMgt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Infrastructure.DIExtensions
{
    public static class DatabaseExtension
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            

            services.AddDbContext<APPContext>(options => options.UseSqlServer(configuration.GetConnectionString("CustMgtConnectionString")));

            services.AddTransient<DbContext, APPContext>();
        }
    }
}
