﻿using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AppointmentViewModel;

[Area("ZeusMedAdmin")]
public class AppointmentController : Controller
{
    private readonly AppDbContext _context;

    public AppointmentController(AppDbContext context)
    {
        _context = context;
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        var appointments = _context.Appointments
            .Include(a => a.Doctor)
            .ToList();

        return View(appointments);
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


            if (User.IsInRole("Admin,Doctor"))
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                return RedirectToAction("Success", "Home", new { area = string.Empty });
            }
        }

        var doctors = _context.Doctors.ToList();
        ViewBag.Doctors = new SelectList(doctors, "Id", "Name", "Surname");
        return View(appointmentVM);
    }







    public IActionResult CreateAppointment()
    {
        var appointmentVM = new AppointmentVM();

        var doctors = _context.Doctors.ToList();
        var services = _context.Services.ToList();

        ViewBag.Doctors = new SelectList(doctors, "Id", "Fullname");
        ViewBag.Services = new SelectList(services, "Id", "Title");
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
    public IActionResult CreateAppointment(AppointmentVM appointmentVM)
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

            return RedirectToAction("Index", "Appointment");
        }

        var doctors = _context.Doctors.ToList();
        ViewBag.Doctors = new SelectList(doctors, "Id", "Name", "Surname");
        return View(appointmentVM);
    }

    public IActionResult Edit(int id)
    {
        var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        var appointmentVM = new AppointmentVM
        {
            Name = appointment.Name,
            Surname = appointment.Surname,
            PhoneNumber = appointment.Phone,
        };

        return View(appointmentVM);
    }

    private bool AppointmentExists(int id)
    {
        return _context.Appointments.Any(a => a.Id == id);
    }
}
