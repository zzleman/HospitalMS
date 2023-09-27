using System;
using System.ComponentModel.DataAnnotations;
using ZeusMed.Core.Entities;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AppointmentViewModel;

public class AppointmentVM
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string ProblemDescription { get; set; }

    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan AppointmentTime { get; set; }
    public int SelectedDoctorId { get; set; }

    public List<Doctor> Doctors { get; set; }

    public string QueueNumber { get; set; }
}

