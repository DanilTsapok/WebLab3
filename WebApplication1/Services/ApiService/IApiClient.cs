using WebApplication1.Models.BookAPIModel;

namespace WebApplication1.Services.ApiService
{
    public interface IApiClient
    {
        Task<KindBooks> GetItems();
    }
}