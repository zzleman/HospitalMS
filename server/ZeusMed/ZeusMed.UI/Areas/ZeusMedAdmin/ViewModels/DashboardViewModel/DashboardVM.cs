using System;
namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DashboardViewModel;

public class DashboardVM
{
    public int DoctorCount { get; set; }
    public int ServiceCount { get; set; }
    public int DonorCount { get; set; }
    public int BirthCount { get; set; }
    public int DeathCount { get; set; }

    public DashboardVM()
    {
        DoctorCount = 0;
        ServiceCount = 0;
        DonorCount = 0;
        BirthCount = 0;
        DeathCount = 0;
    }
}

