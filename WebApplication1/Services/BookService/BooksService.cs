using WebApplication1.Models.BookModel;
using WebApplication1.Models.LoanModel;
using WebApplication1.Models.UserModel;

namespace WebApplication1.Services.BookService
{
    public class BooksService : IBooksServices
    {

        public static List<BookModel> _books = new();
        public BooksService()
        {
            for (int i = 0; i <= 10; i++)
            {
                _books.Add(new BookModel(i, $"Author{i}", $"Title{i}", $"Description{i}",i));
            }
        }
        public Task<BookModel> AddBook(BookModel book)
        {
            _books.Add(book);
            return Task.FromResult(book);
        }

        public Task<List<BookModel>> GetBooks()
        {
            return Task.FromResult(_books);
        }

        public Task<BookModel> DeleteBook(int Id)
        {
            var loan = _books.Find(l => l.Id == Id);
            if (loan != null)
            {
                _books.Remove(loan);
            }
            return Task.FromResult(loan);
        }
        public Task<BookModel> UpdateBook(int Id, BookModel book)
        {
            var index = _books.FindIndex(l => l.Id == Id);
            if (index != -1)
            {
                _books[index] = book;
            }
            return Task.FromResult(book);

        }
        public Task<BookModel> CreateBook(BookModel loan)
        {
            if (!_books.Contains(loan))
            {
                _books.Add(loan);
            }
            return Task.FromResult(loan);

        }

        public Task<BookModel> GetBookById(int Id)
        {
            var user = _books.Find(u => u.Id == Id);
            return Task.FromResult(user);
        }
    }
}
