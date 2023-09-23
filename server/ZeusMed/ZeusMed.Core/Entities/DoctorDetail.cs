using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities
{
    public class DoctorDetail : IEntity
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public Doctor Doctor { get; set; }
    }
}
