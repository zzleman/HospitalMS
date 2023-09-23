
using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.ServiceViewModel;

public class ServicePostVM
{
    [Required, StringLength(40)]
    public string? Title { get; set; } = null!;

    [Required, StringLength(200)]
    public string? Description { get; set; } = null!;

    [Required, StringLength(255)]
    public string? ImagePath { get; set; } = null!;
}

