using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using WebApplication1.Models.UserModel;

namespace WebApplication1.Services.UserService
{
    public class UserService : IUserService
    {
        public List<UserModel> _users = new List<UserModel> { new UserModel
        {
            Id = 1,
            FirstName = "Danil",
            LastName = "Tsapok",
            Email = "danyatsapok200445@gmail.com",
            Password = "$2a$11$8aVockjwHg98J7qoLggEnODJwn/5nl5N3BiDfehB4DRLgCWuCUgdC",
            DayOfBirth = DateTime.UtcNow.AddDays(12),
            LastLogin = DateTime.UtcNow.AddDays(-2),
            FailedLoginAttempts = 0,
        }, new UserModel
        {
            Id =2,
            FirstName ="John",
            LastName="Dou",
            Email="johndoe@gmail.com",
            Password = "$2a$11$Yii5m0KjnG6J6qti14951.hcobYXfntVPpgPFsmmGoQPmd08u5jRy",
            DayOfBirth = DateTime.UtcNow.AddDays(-12),
            LastLogin = DateTime.UtcNow.AddDays(-2),
            FailedLoginAttempts = 0,
        }, new UserModel
        {
            Id =2,
            FirstName ="Jane",
            LastName="Smith",
            Email="janesmith@gmail.com",
            Password = "$2a$11$OWh.EKIqyozz8xbxqvmygOmrXlUvWbyNeVqyVd5jficbKS9Sf2g1K",
            DayOfBirth = DateTime.UtcNow.AddDays(-12),
            LastLogin = DateTime.UtcNow.AddDays(-2),
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

        public UserModel GetUserByEmail(string email)
        {
            var user = _users.Find(u => u.Email == email);
            if (user != null)
            {
                return user;
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
            var existingUser = _users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }
            int maxId = _users.Any() ? _users.Max(u => u.Id) : 0;
            user.Id = maxId+1;
            _users.Add(user);
            return Task.FromResult(user);
        }


    }
}
