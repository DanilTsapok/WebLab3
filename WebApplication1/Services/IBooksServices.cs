using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IBooksServices
    {
        void AddBook(Book book);
        List<Book>  GetBooks();  

    }
}
