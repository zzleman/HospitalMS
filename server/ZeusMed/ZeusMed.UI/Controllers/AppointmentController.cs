using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AppointmentViewModel;

namespace ZeusMed.UI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            var appointmentVM = new AppointmentVM();

            var doctors = _context.Doctors.ToList();

            ViewBag.Doctors = new SelectList(doctors, "Id", "Fullname");
            var timeSlotInterval = TimeSpan.FromMinutes(30);

            var startTime = TimeSpan.FromHours(9);
            var endTime = TimeSpan.FromHours(17.5);

            var timeSlots = new List<SelectListItem>();

            while (startTime <= endTime)
            {
                if (startTime.Hours != 13)
                {
                    timeSlots.Add(new SelectListItem
                    {
                        Value = startTime.ToString("hh\\:mm"),
                        Text = startTime.ToString("hh\\:mm")
                    });
                }

                startTime = startTime.Add(timeSlotInterval);
            }

            ViewBag.TimeSlots = timeSlots;

            return View(appointmentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppointmentVM appointmentVM)
        {
            if (!ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    Name = appointmentVM.Name,
                    Surname = appointmentVM.Surname,
                    Phone = appointmentVM.PhoneNumber,
                    ProblemDescription = appointmentVM.ProblemDescription,
                    AppointmentDate = appointmentVM.AppointmentDate,
                    AppointmentTime = appointmentVM.AppointmentTime,
                };
                appointment.DoctorId = appointmentVM.SelectedDoctorId;

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }

            var doctors = _context.Doctors.ToList();
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name", "Surname");
            return View(appointmentVM);
        }

    }
}
