using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using WebApplication1.Models;
using WebApplication1.Models.BookAPIModel;
using WebApplication1.Models.BookModel;
using WebApplication1.Models.LoanModel;
using WebApplication1.Models.ResponseModel;
using WebApplication1.Services.ApiService;
using WebApplication1.Services.BookService;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

                var response = new ResponseModel<List<BooksModel>>();
                var DataFromApi = await _httpClient.GetItems();
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

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(BookModel book)
        {
            try
            {
                var response = new ResponseModel<BookModel>();
                var AddBook = await _booksServices.CreateBook(book);
                response.Message = "The data is added";
                response.StatusCode = HttpStatusCode.Created;
                response.Data = AddBook;
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

        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById(int Id)
        {
            try
            {
                var response = new ResponseModel<BookModel>();
                var book = await _booksServices.GetBookById(Id);
                if (book != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = book;
                }
                response.Message = "";
                response.StatusCode = HttpStatusCode.NotFound;
                response.Data = book;
                return StatusCode((int)response.StatusCode, response);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = ex.ToString()
                }); ;

            }
        }
        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var response = new ResponseModel<List<BookModel>>();
                var books = await _booksServices.GetBooks();
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

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int Id, BookModel book)
        {
            try
            {
                var response = new ResponseModel<BookModel>();
                var updateBook = await _booksServices.UpdateBook(Id, book);
                if (updateBook != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = book;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.Message = "";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Data = book;
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
        [HttpDelete("DeleteBook") ]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            try
            {
                var response = new ResponseModel<BookModel>();
                var deleteBook = await _booksServices.DeleteBook(Id);
                if (deleteBook != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = deleteBook;
                    return StatusCode((int)response.StatusCode, response);
                }
                return StatusCode((int)response.StatusCode, response);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseModel<string>
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = ex.ToString()
                });
            }
        }
    }
}

