using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel;

public class LoginVM
{
    [Required]
    [Display(Name = "Email or Username")]
    public string EmailOrUsername { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

}