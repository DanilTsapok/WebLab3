using Microsoft.Extensions.Configuration;
using WebApplication1.Models.BookAPIModel;

namespace WebApplication1.Services.ApiService
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<KindBooks> GetItems()
        {
            var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=reactr:keyes&{_configuration.GetSection("API_KEY").Value}=yourAPIKey");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<KindBooks>();
        }




    }
}
