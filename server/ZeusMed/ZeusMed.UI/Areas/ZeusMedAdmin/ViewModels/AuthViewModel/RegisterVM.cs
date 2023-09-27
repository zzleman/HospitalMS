using System;
using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel
{
    public class RegisterVM
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords aren't the same.")]
        public string ConfirmPassword { get; set; }
    }
}
