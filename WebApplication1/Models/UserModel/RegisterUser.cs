using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.UserModel
{
    public class RegisterUser
    {

        [Required(ErrorMessage = "Input your first name")]
        [StringLength(15, ErrorMessage = "Max length first name is 15 symbols")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Input your last name")]
        [StringLength(15, ErrorMessage = "Max length last name is 15 symbols")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Input correct email")]
        [EmailAddress(ErrorMessage = "Input correct email")]
        public string? Email { get; set; }

        public DateTime? DayOfBirth { get; set; }
        public string? Password { get; set; }

        public RegisterUser() { }
    }

}

