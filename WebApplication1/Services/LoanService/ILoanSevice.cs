using WebApplication1.Models.LoanModel;

namespace WebApplication1.Services.LoanService
{
    public interface ILoanSevice
    {
        Task<List<LoanModel>> GetAllLoans();
        Task <LoanModel> GetLoanById(int id);
        Task<LoanModel> CreateLoan(LoanModel loan);
        Task<LoanModel> UpdateLoan(int Id, LoanModel loan);
        Task<LoanModel> DeleteLoan(int id);
    }
}
