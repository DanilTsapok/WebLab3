namespace WebApplication1.Models.LoanModel
{
    public class LoanModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LocanDate { get; set; }

        public DateTime ReturnDate { get; set; }    


        public LoanModel(int Id, int UserId, int BookId, DateTime LocanDate, DateTime ReturnDate)
        {
            this.Id = Id;
            this.UserId = UserId;   
            this.BookId = BookId;
            this.LocanDate = LocanDate;
            this.ReturnDate = ReturnDate;
        }
        public LoanModel() { }

    }
}
