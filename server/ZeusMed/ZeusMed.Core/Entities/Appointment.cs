using System;
using System.ComponentModel.DataAnnotations;
using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = null!;

        [Required]
        public string? Surname { get; set; } = null!;

        [Required]
        public string? Phone { get; set; } = null!;

        [Required]
        public int DoctorId { get; set; }

        public string ProblemDescription { get; set; }

        public DateTime AppointmentDate { get; set; }

        public TimeSpan AppointmentTime { get; set; }

        public Doctor Doctor { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public List<Service> Services { get; set; }
    }
}
