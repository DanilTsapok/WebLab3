using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models.ResponseModel;
using WebApplication1.Models.UserModel;
using WebApplication1.Services.AuthService;
using WebApplication1.Services.UserService;

namespace WebApplication1.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;


        public AuthService(IConfiguration configuration, IUserService userService, IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public Task<UserModel> Register(RegisterUser user)
        {
            var hashedPassword = _passwordHasher.Generate(user.Password);
            var newUser = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DayOfBirth = user.DayOfBirth,
                Email=user.Email,
                Password = hashedPassword,

            };
            return Task.FromResult(newUser);
        }


        public Task<string> Login(LoginUser user)
        {
            return CheckedUser(user);
        }

        public Task<string> CheckedUser(LoginUser user)
        {
            var existingUser = _userService.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return GenerateJSONWebToken(user);
            }
            throw new Exception();
        }
        public Task<string> GenerateJSONWebToken(LoginUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email.ToString())
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: credentials
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult<string>(tokenValue.ToString());

        }



    }
}
