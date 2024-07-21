using CustomerMgt.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace CustomerMgt.MVC.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CustomerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        

        public async Task<ResponseModel<Page<CustomerModel>>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
           

            try
            {

                var url = $"{_baseUrl}api/customer/{pageNumber}/{pageSize}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(result))
                {
                    return new ResponseModel<Page<CustomerModel>>
                    {
                        RequestSuccessful = false,
                        Message = "No content received from the server"
                    };
                }

                try
                {
                    var responseModel = JsonConvert.DeserializeObject<ResponseModel<Page<CustomerModel>>>(result);

                    if (responseModel == null)
                    {
                        return new ResponseModel<Page<CustomerModel>>
                        {
                            RequestSuccessful = false,
                            ResponseCode = "500",
                            Message = "Deserialization returned null"
                        };
                    }

                    return responseModel;
                }
                catch (JsonException jsonEx)
                {
                    // Handle JSON deserialization errors
                    return new ResponseModel<Page<CustomerModel>>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "500",
                        Message = $"Error deserializing response: {jsonEx.Message}"
                    };
                }

            }
            catch (HttpRequestException ex) when (ex.InnerException is IOException)
            {
                // Handle network-related errors
                return new ResponseModel<Page<CustomerModel>>
                {
                    RequestSuccessful = false,
                    ResponseCode = "500",
                    Message = $"An error occurred while fetching customers: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                // Handle other types of errors
                return new ResponseModel<Page<CustomerModel>>
                {
                    RequestSuccessful = false,
                    ResponseCode = "500",
                    Message = $"An error occurred while fetching customers: {ex.Message}"
                };
            }
        }




        public async Task<ResponseModel<CustomerModel>> GetCustomerByIdAsync(long id)
        {

            try
            {
                var url = $"{_baseUrl}api/customer/byId?id={id}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(result))
                {
                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        Message = "No content received from the server"
                    };
                }

                try
                {
                    var responseModel = JsonConvert.DeserializeObject<ResponseModel<CustomerModel>>(result);

                    
                    if (responseModel == null)
                    {
                        return new ResponseModel<CustomerModel>
                        {
                            RequestSuccessful = false,
                            ResponseCode = "500",
                            Message = "Deserialization returned null"
                        };
                    }

                    return responseModel;
                }
                catch (JsonException jsonEx)
                {
                    
                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "500",
                        Message = $"Error deserializing response: {jsonEx.Message}"
                    };
                }
            }
            catch (HttpRequestException ex) when (ex.InnerException is IOException)
            {
                // Handle network-related errors
                return new ResponseModel<CustomerModel>
                {
                    RequestSuccessful = false,
                    ResponseCode = "500",
                    Message = $"An error occurred while fetching the customer: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                // Handle other types of errors
                return new ResponseModel<CustomerModel>
                {
                    RequestSuccessful = false,
                    ResponseCode = "500",
                    Message = $"An error occurred while fetching the customer: {ex.Message}"
                }; ;
            }


        }

        public async Task<ResponseModel<CustomerModel>> CreateCustomerAsync(CustomerModel customer)
        {

            
            try
            {
                var url = $"{_baseUrl}api/customer";
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(result))
                {
                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        Message = "No content received from the server"
                    };
                }

                try
                {
                    var responseModel = JsonConvert.DeserializeObject<ResponseModel<CustomerModel>>(result);


                    if (responseModel == null)
                    {
                        return new ResponseModel<CustomerModel>
                        {
                            RequestSuccessful = false,
                            ResponseCode = "500",
                            Message = "Deserialization returned null"
                        };
                    }

                    return responseModel;
                }
                catch (JsonException jsonEx)
                {

                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "500",
                        Message = $"Error deserializing response: {jsonEx.Message}"
                    };
                }

            }
            catch (HttpRequestException ex) when (ex.InnerException is IOException)
            {
                // Handle network-related errors
                return new ResponseModel<CustomerModel> { RequestSuccessful = false, ResponseCode = "500", Message = $"An error occurred while creating customer: {ex.Message}" };
            }
            catch (Exception error)
            {
                // Handle other types of errors
                return new ResponseModel<CustomerModel>
                {
                    RequestSuccessful = false,
                    ResponseCode = "500",
                    Message = $"An error occurred while creating customer: {error.Message}"
                }; ;
            }
            
        }

        public async Task<ResponseModel<CustomerModel>> UpdateCustomerAsync( CustomerModel customer)
        {

            try
            {
                
                var url = $"{_baseUrl}api/customer/update?id={customer.Id}";
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{url}", content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(result))
                {
                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        Message = "No content received from the server"
                    };
                }

                try
                {
                    var responseModel = JsonConvert.DeserializeObject<ResponseModel<CustomerModel>>(result);


                    if (responseModel == null)
                    {
                        return new ResponseModel<CustomerModel>
                        {
                            RequestSuccessful = false,
                            ResponseCode = "500",
                            Message = "Deserialization returned null"
                        };
                    }

                    return responseModel;
                }
                catch (JsonException jsonEx)
                {

                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "500",
                        Message = $"Error deserializing response: {jsonEx.Message}"
                    };
                }
            }
            catch (HttpRequestException ex) when (ex.InnerException is IOException)
            {
                // Handle network-related errors
                return new ResponseModel<CustomerModel> { RequestSuccessful = false, ResponseCode = "500", Message = $"An error occurred while updating the customer: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new ResponseModel<CustomerModel>
                {
                    RequestSuccessful = false,
                    ResponseCode = "500",
                    Message = $"An error occurred while updating the customer: {ex.Message}"
                };
            }
            
        }

        public async Task<ResponseModel<CustomerModel>> DeleteCustomerAsync(long id)
        {

            try
            {
                var url = $"{_baseUrl}api/customer/delete?id={id}";
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{url}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(result))
                {
                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        Message = "No content received from the server"
                    };
                }

                try
                {
                    var responseModel = JsonConvert.DeserializeObject<ResponseModel<CustomerModel>>(result);


                    if (responseModel == null)
                    {
                        return new ResponseModel<CustomerModel>
                        {
                            RequestSuccessful = false,
                            ResponseCode = "500",
                            Message = "Deserialization returned null"
                        };
                    }

                    return responseModel;
                }
                catch (JsonException jsonEx)
                {

                    return new ResponseModel<CustomerModel>
                    {
                        RequestSuccessful = false,
                        ResponseCode = "500",
                        Message = $"Error deserializing response: {jsonEx.Message}"
                    };
                }
            }
            catch (HttpRequestException ex) when (ex.InnerException is IOException)
            {
                // Handle network-related errors
                return new ResponseModel<CustomerModel> { RequestSuccessful = false, ResponseCode = "500", Message = $"An error occurred while deleting the customer: {ex.Message}" };
            }
            catch (Exception ex)
            {
                // Handle other types of errors
                return new ResponseModel<CustomerModel> { RequestSuccessful = false, ResponseCode = "500", Message = $"An error occurred while deleting the customer: {ex.Message}" };
            }
            
        }
    }
}
