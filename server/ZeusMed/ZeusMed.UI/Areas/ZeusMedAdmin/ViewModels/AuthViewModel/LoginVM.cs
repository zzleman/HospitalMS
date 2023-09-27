using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel;

public class LoginVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

}