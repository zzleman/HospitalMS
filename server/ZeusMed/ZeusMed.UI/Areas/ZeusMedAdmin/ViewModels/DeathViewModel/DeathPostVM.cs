using ZeusMed.Core.Entities;
using ZeusMed.Core.Enums;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DeathViewModel;

public class DeathPostVM
{
    public string Fullname { get; set; } = null!;

    public DateTime DeathTime { get; set; }

    public string? DeathPlace { get; set; } = null!;

    public string? CauseOfDeath { get; set; } = null!;

    public Gender ChosenGender { get; set; }

    public Doctor Doctor { get; set; }


    public int DoctorId { get; set; }
}

