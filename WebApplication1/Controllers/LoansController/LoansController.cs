using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models.LoanModel;
using WebApplication1.Models.ResponseModel;
using WebApplication1.Services.LoanService;

namespace WebApplication1.Controllers.LoansController
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanSevice _loanSevice;

        public LoansController(ILoanSevice loanService)
        {
            _loanSevice = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            try
            {
                var response = new ResponseModel<List<LoanModel>>();
                var loans = await _loanSevice.GetAllLoans();
                response.Message = "";
                response.StatusCode = HttpStatusCode.OK;
                response.Data = loans;
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

        [HttpGet("Id")]
        public async Task<IActionResult> GetLoanById(int Id)
        {
            try
            {
                var response = new ResponseModel<LoanModel>();
                var loan = await _loanSevice.GetLoanById(Id);
                if (loan != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = loan;
                }
                response.Message = "";
                response.StatusCode = HttpStatusCode.NotFound;
                response.Data = loan;
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

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] LoanModel loan)
        {
            try
            {
                var response = new ResponseModel<LoanModel>();
                var newLoan = await _loanSevice.CreateLoan(loan);
                if (newLoan != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = newLoan;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.Message = "";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Data = loan;
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

        [HttpPut("UpdateLoan")]
        public async Task<IActionResult> UpdateLoan(int Id, LoanModel loan)
        {
            try
            {
                var response = new ResponseModel<LoanModel>();
                var updateLoan = await _loanSevice.UpdateLoan(Id, loan);
                if (updateLoan != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = loan;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.Message = "";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Data = loan;
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

        [HttpDelete("DeleteLoan")]
        public async Task<IActionResult> DeleteLoan(int Id)
        {
            try
            {
                var response = new ResponseModel<LoanModel>();
                var deleteLoan = await _loanSevice.DeleteLoan(Id);
                if (deleteLoan != null)
                {
                    response.Message = "";
                    response.StatusCode = HttpStatusCode.OK;
                    response.Data = deleteLoan;
                    return StatusCode((int)response.StatusCode, response);
                }
                response.Message = "";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Data = deleteLoan;
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
    }
}
