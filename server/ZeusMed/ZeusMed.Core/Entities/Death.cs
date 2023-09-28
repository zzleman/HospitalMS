using ZeusMed.Core.Enums;
using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities
{
    public class Death : IEntity
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = null!;

        public DateTime DeathTime { get; set; }

        public string? DeathPlace { get; set; } = null!;

        public string? CauseOfDeath { get; set; } = null!;

        public Gender ChosenGender { get; set; }

        public Doctor Doctor { get; set; }

        public int DoctorId { get; set; }


    }
}
