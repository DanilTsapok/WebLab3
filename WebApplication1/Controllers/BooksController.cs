using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using WebApplication1.Models;
using WebApplication1.Services;
using static System.Reflection.Metadata.BlobBuilder;

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

        [HttpGet("BooksAPI")]
        public async Task<IActionResult> GetBooksApi()
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

        [HttpPost("PostAddBook")]
        public async Task<IActionResult> PostAddBook(Book book)
        {
            try
            {
                var response = new ResponseModel<Book>();
                _booksServices.AddBook(book);
                response.Message = "The data is added";
                response.StatusCode = HttpStatusCode.Created;
                response.Data = null;
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

    [HttpGet("GetAddBooks")]
    public async Task<IActionResult> GetAddBooks()
    {
        try
        {
            var response = new ResponseModel<Book>();
            var books = _booksServices.GetBooks();
            response.Message = "The data is retrieved";
            response.StatusCode = HttpStatusCode.OK;
            response.Data = books;
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

