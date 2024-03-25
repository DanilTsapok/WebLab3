using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models;
using WebApplication1.Models.ResponseModel;
using WebApplication1.Models.UserModel;
using WebApplication1.Services.ApiService;
using WebApplication1.Services.UserService;


namespace WebApplication1.Controllers.UserController
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        [Authorize]
        [HttpGet("Id")]
        public async Task<IActionResult> GetUser(int Id)
        {
            try
            {
                var response = new ResponseModel<UserModel>();
                var user = await _userService.GetUserById(Id);
                if (user != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = user;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.Message = "User is not found";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Data = user;
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = ex.ToString()
                });

            }
        }
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = new ResponseModel<List<UserModel>>();
                var users = await _userService.GetAllUsers();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Users are found";
                response.Data = users;
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,

                });
            }
        }
        [Authorize]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> PostCreateUser([FromBody] UserModel? user)
        {

            try
            {
                var response = new ResponseModel<UserModel>();
                var newUser = await _userService.CreateUser(user);
                if (newUser != null)
                {
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "User is add";
                    response.Data = user;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "User is not add";
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,

                });
            }
        }
        [Authorize]
        [HttpPut("Id")]
        public async Task<IActionResult> UpdateUser(int Id, UserModel user)
        {
            try
            {
                var response = new ResponseModel<UserModel>();
                var userCurrent = await _userService.UpdateUser(Id, user);
                if (userCurrent != null)
                {
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "User updated";
                    response.Data = user;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "User is not found";
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,

                });
            }
        }
        [Authorize]
        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                var response = new ResponseModel<UserModel>();
                var user = await _userService.DeleteUser(Id);
                if (user != null)
                {

                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "User is delete";
                    response.Data = user;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "User is not found";
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,

                });
            }

        }
    }
}
