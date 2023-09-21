using System;
using System.ComponentModel.DataAnnotations;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel;

public class DoctorPostVM
{
    [Required,StringLength(70)]
    public string? Fullname { get; set; } = null!;

    [Required, StringLength(40)]
    public string? Department { get; set; } = null!;

    [Required, StringLength(255)]
    public string? ImagePath { get; set; } = null!;
}

