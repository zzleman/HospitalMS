using ZeusMed.Core.Enums;
using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities
{
    public class Donor : IEntity
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = null!;

        public int Age { get; set; }

        public Gender ChosenGender { get; set; }

        public BloodGroup ChosenBloodGroup { get; set; }

        public string? Quantity { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;

    }
}
