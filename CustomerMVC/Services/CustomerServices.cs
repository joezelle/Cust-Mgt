
using CustomerMgt.MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.MVC.Services
{
    public class CustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CustomerResponse> GetCustomersAsync(int pageNumber, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"api/customers?pageNumber={pageNumber}&pageSize={pageSize}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CustomerResponse>(json);


            //var response = await _httpClient.GetStringAsync($"api/customer?pageNumber={pageNumber}&pageSize={pageSize}");
            //return JsonConvert.DeserializeObject<IEnumerable<Customer>>(response);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://yourapiurl/api/customers/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Customer>(json);

            //var response = await _httpClient.GetStringAsync($"api/customer/{id}");
            //return JsonConvert.DeserializeObject<Customer>(response);
        }

        //public async Task CreateCustomerAsync(Customer customer)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
        //    await _httpClient.PostAsync("api/customer", content);
        //}

        //public async Task UpdateCustomerAsync(Customer customer)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
        //    await _httpClient.PutAsync($"api/customer/{customer.Id}", content);
        //}

        //public async Task DeleteCustomerAsync(int id)
        //{
        //    await _httpClient.DeleteAsync($"api/customer/{id}");
        //}
    }
}

