using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class BooksService: IBooksServices
    {

        public List<Book> books = new List<Book>();
        public bool AddBook(Book book)
        {
            if (!books.Contains(book))
            {
                books.Add(book);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
