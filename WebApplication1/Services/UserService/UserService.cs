using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models.UserModel;

namespace WebApplication1.Services.UserService
{
    public class UserService : IUserService
    {
        public static List<UserModel> _users = new List<UserModel> { new UserModel
        {
            Id = 1,
            FirstName = "Danil",
            LastName = "Tsapok",
            Email = "danyatsapok200445@gmail.com",
            Password = "123",
            DayOfBirth = DateTime.Now.AddDays(12),  
            LastLogin = DateTime.Now,
            FailedLoginAttempts = 0, 
        } 
 
        };



        //public UserService()
        //{
        //    for (int i = 0; i <= 10; i++)
        //    {
        //        _users.Add(new UserModel(i, $"FirstName{i}", $"LastName{i}", $"Test{i}@gmail.com", DateTime.Now.AddDays(-i), $"123{i}", DateTime.Now.AddDays(-i), i));
        //    }
        //}

        public Task<List<UserModel>> GetAllUsers()
        {
            return Task.FromResult(_users);
        }

        public Task<UserModel> GetUserById(int Id)
        {
            var user = _users.Find(u => u.Id == Id);
            return Task.FromResult(user);
        }

        public Task<UserModel> GetUserByEmail(string email)
        {
            var user = _users.Find(u => u.Email == email);
            if(user != null)
            {
            return Task.FromResult(user);
            }
            throw new Exception();
        }

        public Task<UserModel> UpdateUser(int Id, UserModel user)
        {

            var index = _users.FindIndex(u => u.Id == Id);
            if (index != -1)
            {
                _users[index] = user;
            }
            return Task.FromResult(user);
        }

        public Task<UserModel> DeleteUser(int Id)
        {
            var user = _users.Find(u => u.Id == Id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return Task.FromResult(user);
        }

        public Task<UserModel> CreateUser(UserModel user)
        {
            if (!_users.Contains(user))
            {
            _users.Add(user);
            }
            return Task.FromResult(user);
        }

      
    }
}
