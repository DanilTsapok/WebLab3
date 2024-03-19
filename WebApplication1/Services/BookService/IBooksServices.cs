using WebApplication1.Models.BookModel;

namespace WebApplication1.Services.BookService
{
    public interface IBooksServices
    {
        Task<BookModel> CreateBook(BookModel book);
        Task<List<BookModel>> GetBooks();

        Task<BookModel> GetBookById(int Id);
        Task<BookModel> UpdateBook(int Id,BookModel book);

        Task<BookModel> DeleteBook(int Id);
    }
}
