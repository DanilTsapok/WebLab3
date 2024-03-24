using WebApplication1.Models.UserModel;

namespace WebApplication1.Services.UserService
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> DeleteUser(int Id);
        Task<List<UserModel>> GetAllUsers();
        UserModel GetUserByEmail(string email);
        Task<UserModel> GetUserById(int Id);
        Task<UserModel> UpdateUser(int Id, UserModel user);
    }
}