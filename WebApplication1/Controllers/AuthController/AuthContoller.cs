
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models.AuthResponseModel;
using WebApplication1.Models.ResponseModel;
using WebApplication1.Models.UserModel;
using WebApplication1.Services.AuthServices;
using WebApplication1.Services.UserService;

namespace WebApplication1.Controllers.AuthController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthContoller : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthContoller(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            try
            {
                var response = new AuthResponseModel<LoginUser>();
                var token = await _authService.Login(user);
                response.StatusCode = HttpStatusCode.OK;
                response.AccessToken = token;
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                });

            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            try
            {
                var response = new AuthResponseModel<UserModel>();
                var newUser = await _authService.Register(user);
                await _userService.CreateUser(newUser);
                response.StatusCode = HttpStatusCode.OK;
                response.Data = newUser;
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null
                });

            }
        }
    }
}

