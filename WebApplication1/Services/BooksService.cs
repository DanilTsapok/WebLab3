using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class BooksService: IBooksServices
    {

        public  List<Book> books = new ();
        public void AddBook(Book book)
        {
             books.Add(book);
        }

        public List<Book> GetBooks()
        {
            return books;
        }
       
    }
}
