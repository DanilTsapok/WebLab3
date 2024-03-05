namespace WebApplication1.Services
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string url);
    }
}
