using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models.ResponseModel;
using WebApplication1.Models.UserModel;
using WebApplication1.Services.PasswordService;
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

        public async Task<UserModel> Register(RegisterUser user)
        {
            var hashedPassword = _passwordHasher.Generate(user.Password);
            var newUser = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DayOfBirth = user.DayOfBirth,
                Email = user.Email,
                Password = hashedPassword,
            };
            await _userService.CreateUser(newUser);
            return newUser;
        }


        public Task<string> Login(LoginUser user)
        {
            var existingUser = _userService.GetUserByEmail(user.Email);

            var result = _passwordHasher.Verify(user.Password, existingUser.Password);
  
            if (result)
            {
                existingUser.LastLogin = DateTime.Now;
                return GenerateJSONWebToken(user);
            }
            existingUser.FailedLoginAttempts += 1;
            Console.WriteLine(existingUser.FailedLoginAttempts);
            return null;
            
        }

        public Task<string> GenerateJSONWebToken(LoginUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["Key"]));
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
