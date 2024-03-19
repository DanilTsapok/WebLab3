namespace WebApplication1.Models.BookModel
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public BookModel(int Id,string Author, string Title, string Description, int Price)
        {
            this.Id = Id;
            this.Author = Author;
            this.Title = Title;
            this.Description = Description;
            this.Price = Price;
        }

        public BookModel() { }
    }
}
