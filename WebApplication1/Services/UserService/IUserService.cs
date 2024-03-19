﻿using WebApplication1.Models.UserModel;

namespace WebApplication1.Services.UserService
{
    public interface IUserService
    {

        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> GetUser(int Id);
        Task<UserModel> UpdateUser(int Id, UserModel user);
        Task<UserModel> DeleteUser(int Id);
    }
}