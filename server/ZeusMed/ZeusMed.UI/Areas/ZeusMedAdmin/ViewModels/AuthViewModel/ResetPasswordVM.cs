
using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel;

public class ResetPasswordVM
{
    [Required, DataType(DataType.Password)]
    public string? NewPassword { get; set; }
    [Required, DataType(DataType.Password), Compare(nameof(NewPassword))]
    public string? ConfirmNewPassword { get; set; }
}