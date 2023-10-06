using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel;

public class ForgotPasswordVM
{
    [Required, MaxLength(256), DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
}

