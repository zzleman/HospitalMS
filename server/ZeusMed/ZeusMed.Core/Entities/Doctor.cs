using ZeusMed.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeusMed.Core.Entities
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }

        public string? Fullname { get; set; } = null!;

        public string? ImagePath { get; set; } = null!;


        // Use data annotations to specify the foreign key
        [ForeignKey("AssociatedService")]
        public int AssociatedServiceId { get; set; }
        public Service AssociatedService { get; set; }
    }
}
