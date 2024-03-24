using WebApplication1.Models.UserModel;

namespace WebApplication1.Services.AuthServices
{
    public interface IAuthService
    {
        Task<string> GenerateJSONWebToken(LoginUser user);
        Task<string> Login(LoginUser user);
        Task<UserModel> Register(RegisterUser user);
    }
}