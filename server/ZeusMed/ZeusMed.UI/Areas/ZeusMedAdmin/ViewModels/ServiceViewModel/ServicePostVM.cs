
using System.ComponentModel.DataAnnotations;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.ServiceViewModel;

public class ServicePostVM
{
    [Required, StringLength(40)]
    public string? Title { get; set; } = null!;

    [Required, StringLength(200)]
    public string? Description { get; set; } = null!;

    [Required]
    public IFormFile Image { get; set; }

    public ServiceDetail ServiceDetail { get; set; }
}

