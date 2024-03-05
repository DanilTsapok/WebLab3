using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IApiClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IBooksServices _booksServices;
        public BooksController(IConfiguration configuration, IApiClient httpClient, IBooksServices booksServices)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _booksServices = booksServices;
        }

        [HttpGet("Books")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {

                var response = new ResponseModel<BooksModel>();
                var DataFromApi = await _httpClient.GetAsync<KindBooks>($"https://www.googleapis.com/books/v1/volumes?q=reactr:keyes&{_configuration.GetSection("API_KEY").Value}=yourAPIKey");
                response.Data = DataFromApi.Items;
                response.Message = "Good response";
                response.StatusCode = HttpStatusCode.OK;
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

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(Book book)
        {
            try
            {

                var response = new ResponseModel<string>();
                if (_booksServices.AddBook(book))
                {
                    response.Message = "The data is added";
                    response.StatusCode = HttpStatusCode.Created;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.Message = "The data already exists";
                response.StatusCode = HttpStatusCode.BadRequest;
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
