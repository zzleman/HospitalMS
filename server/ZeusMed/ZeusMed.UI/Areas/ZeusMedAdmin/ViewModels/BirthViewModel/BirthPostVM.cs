using ZeusMed.Core.Entities;
using ZeusMed.Core.Enums;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.BirthViewModel;

public class BirthPostVM
{

    public string Fullname { get; set; } = null!;

    public string Mom { get; set; } = null!;

    public Gender ChosenGender { get; set; }

    public BloodGroup ChosenBloodGroup { get; set; }

    public Doctor Doctor { get; set; }
}

