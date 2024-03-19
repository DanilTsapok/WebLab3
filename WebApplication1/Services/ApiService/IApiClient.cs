namespace WebApplication1.Services.ApiService
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string url);
    }
}
