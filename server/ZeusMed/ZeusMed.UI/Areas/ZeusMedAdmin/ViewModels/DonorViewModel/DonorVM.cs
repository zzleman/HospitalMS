using System;
using System.ComponentModel.DataAnnotations;
using ZeusMed.Core.Enums;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DonorViewModel;

public class DonorVM
{

    public int Age { get; set; }
    [Required]
    public string Fullname { get; set; } = null!;

    public Gender ChosenGender { get; set; }

    [Required]
    public BloodGroup ChosenBloodGroup { get; set; }

    [Required]
    public string? Quantity { get; set; } = null!;

    [Required]
    public string? PhoneNumber { get; set; } = null!;

}

