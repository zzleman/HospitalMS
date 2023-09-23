using ZeusMed.Core.Entities;

namespace ZeusMed.UI.ViewModels
{
    public class DoctorVM
    {
        public string Fullname { get; set; }

        public string Department { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; } = null!;
    }

}
