using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models.LoanModel;

namespace WebApplication1.Services.LoanService
{
    public class LoanService:ILoanSevice
    {
       private static List<LoanModel> _loans = new();

        public LoanService() { 
            for(int i=0; i<=10; i++)
            {
                _loans.Add(new LoanModel(i, i++,i+2, DateTime.Now.AddDays(-i),DateTime.Now.AddDays(i)));
            }
        }
          
        public Task<List<LoanModel>> GetAllLoans()
        {
            return Task.FromResult(_loans);
        }
        public Task<LoanModel> GetLoanById(int id)
        {
            var loan = _loans.Find(x => x.Id == id);
            return Task.FromResult(loan);
        }

        public Task<LoanModel> CreateLoan (LoanModel loan)
        {
            if (!_loans.Contains(loan))
            {
                _loans.Add(loan);
            }
            return Task.FromResult(loan);
          
        }
        public Task<LoanModel> UpdateLoan(int Id, LoanModel loan)
        {
            var index = _loans.FindIndex(l => l.Id == Id);
            if(index != -1)
            {
                _loans[index] = loan;
            }
            return Task.FromResult(loan);

        }
        public Task <LoanModel> DeleteLoan(int Id)
        {
            var loan = _loans.Find(l => l.Id == Id);
            if (loan != null)
            {
                _loans.Remove(loan);
            }
            return Task.FromResult(loan);
        }

       
    }
}
