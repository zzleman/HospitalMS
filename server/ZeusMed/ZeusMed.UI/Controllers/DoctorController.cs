using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.ViewModels;

namespace ZeusMed.UI.Controllers;

public class DoctorController : Controller
{
    private readonly AppDbContext _context;

    public DoctorController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var doctors = _context.Doctors.Include(d => d.AssociatedService).ToList();

        var doctorViewModel = new DoctorVM
        {
            Doctors = doctors
        };

        return View(doctorViewModel);
    }



}


