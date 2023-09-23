using ZeusMed.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeusMed.Core.Entities
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }

        public string? Fullname { get; set; } = null!;

        public string? ImagePath { get; set; } = null!;

        [ForeignKey("AssociatedService")]
        public int AssociatedServiceId { get; set; }
        public Service AssociatedService { get; set; }

        public DoctorDetail DoctorDetail { get; set; }
    }
}
