﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.UserModel
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
