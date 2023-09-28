using ZeusMed.Core.Enums;
using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities
{
    public class Birth : IEntity
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string Mom { get; set; } = null!;

        public Gender ChosenGender { get; set; }

        public BloodGroup ChosenBloodGroup { get; set; }

        public Doctor Doctor { get; set; }

        public int DoctorId { get; set; }


    }
}
