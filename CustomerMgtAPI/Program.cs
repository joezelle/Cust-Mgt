using Microsoft.Extensions.Configuration;
using CustomerMgt.Infrastructure.DIExtensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using CustomerMgt.Core.Services;
using CustomerMgt.Infrastructure.Filters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NLog;
using NLog.Web;


var builder = WebApplication.CreateBuilder(args);


// NLog 
builder.Logging.ClearProviders(); 
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); 
builder.Host.UseNLog(); 

// Add services to the container.

IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddAppServices();
builder.Services.AddDatabaseServices(configuration);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Management", Version = "v1" });
});




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:44342", "https://localhost:44342") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});




var app = builder.Build();

app.UseExceptionHandler(builder =>
{
    builder.Run(
        async context =>
        {
            var error = context.Features.Get<IExceptionHandlerFeature>();
            var exception = error.Error;

            var logger = context.RequestServices.GetService<ILoggerService?>();
            logger.LogError(exception, "error occured");

            var (responseModel, statusCode) = GlobalExceptionFilter.GetStatusCode<object>(exception);
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var responseJson = JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            await context.Response.WriteAsync(responseJson);
        });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
