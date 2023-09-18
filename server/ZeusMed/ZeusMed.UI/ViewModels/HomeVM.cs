using ZeusMed.Core.Entities;

namespace ZeusMed.UI.ViewModels;

public class HomeVM
{
    public IEnumerable<Service> Services { get; set; } = null!;

    public IEnumerable<Doctor> Doctors { get; set; } = null!;

}

