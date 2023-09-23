using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.ViewModels;

namespace ZeusMed.UI.Controllers;

public class DoctorDetailController : Controller
{
    private readonly AppDbContext _context;

    public DoctorDetailController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int doctorDetailId)
    {

        var doctorDetail = _context.DoctorDetails
                                  .Include(d => d.Doctor)
                                  .ThenInclude(d => d.AssociatedService)
                                  .FirstOrDefault(d => d.Id == doctorDetailId);

        if (doctorDetail == null)
        {
            return NotFound();
        }

        var doctorVM = new DoctorVM
        {
            Fullname = doctorDetail.Doctor.Fullname,
            Department = doctorDetail.Doctor.AssociatedService.Title,
            Description = doctorDetail.Description,
            ImagePath = doctorDetail.Doctor.ImagePath
        };

        return View(doctorVM);
    }
}




