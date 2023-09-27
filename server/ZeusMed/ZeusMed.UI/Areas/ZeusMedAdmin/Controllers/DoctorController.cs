
using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public DoctorController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
    }


    public async Task<IActionResult> Index()
    {
        var doctorsWithDetails = await _context.Doctors
            .Include(d => d.AssociatedService)
            .Include(d => d.DoctorDetail) 
            .ToListAsync();

        return View(doctorsWithDetails);
    }


    [HttpGet]
    public IActionResult Create()
    {
        var services = _context.Services.ToList();

        var viewModel = new DoctorPostVM
        {
            Services = services
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorPostVM doctorPost)
    {
        if (!doctorPost.Image.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "Select correct image format");
            return View(doctorPost);
        }
        if (doctorPost.Image.Length / 1024 > 100)
        {
            ModelState.AddModelError("Image", "Size must be less than 100kb");
        }
        string fileName = Guid.NewGuid().ToString() + doctorPost.Image.FileName;
        string filePath = Path.Combine("assets", "img", fileName);
        string path = Path.Combine(_env.WebRootPath, filePath);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await doctorPost.Image.CopyToAsync(fileStream);
        }

        if (!ModelState.IsValid)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorPost);

            doctor.AssociatedService = _context.Services.Find(doctorPost.AssociatedServiceId);
            await _context.Doctors.AddAsync(doctor);
            doctor.ImagePath = filePath;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var services = _context.Services.ToList();
        doctorPost.Services = services;
        return View(doctorPost);
    }



    public async Task<IActionResult> Delete(int Id)
    {
        Doctor? doctordb = await _context.Doctors.FindAsync(Id);
        if (doctordb == null)
        {
            return NotFound();
        }
        return View(doctordb);
    }
    [HttpPost]
    [ActionName("Delete")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeletePost(int Id)
    {
        Doctor? doctordb = await _context.Doctors.FindAsync(Id);
        if (doctordb == null)
        {
            return NotFound();
        }
        _context.Doctors.Remove(doctordb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAllDoctors()
    {
        // Find all doctors
        var doctors = await _context.Doctors.ToListAsync();

        foreach (var doctor in doctors)
        {
            // Find and remove associated appointments
            var appointmentsToDelete = _context.Appointments.Where(a => a.DoctorId == doctor.Id);
            _context.Appointments.RemoveRange(appointmentsToDelete);
        }

        // Remove all doctors
        _context.Doctors.RemoveRange(doctors);

        // Save changes
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Update(int Id)
    {
        Doctor doctor = await _context.Doctors
            .Include(d => d.DoctorDetail)
            .FirstOrDefaultAsync(d => d.Id == Id);

        if (doctor == null)
        {
            return NotFound();
        }

        List<Service> services = await _context.Services.ToListAsync();

        ViewBag.Services = new SelectList(services, "Id", "Title", doctor.AssociatedServiceId);

        return View(doctor);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int Id, Doctor doctor)
    {
        if (Id != doctor.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            var doctordb = await _context.Doctors
                .Include(d => d.DoctorDetail)
                .FirstOrDefaultAsync(d => d.Id == Id);

            if (doctordb == null)
            {
                return NotFound();
            }

            doctordb.Fullname = doctor.Fullname;

            if (!string.IsNullOrEmpty(doctor.ImagePath))
            {
                doctordb.ImagePath = doctor.ImagePath;
            }

            if (doctordb.DoctorDetail != null)
            {
                doctordb.DoctorDetail.Description = doctor.DoctorDetail.Description;
            }

            var associatedService = _context.Services.Find(doctor.AssociatedServiceId);
            if (associatedService != null)
            {
                doctordb.AssociatedService = associatedService;
            }

            _context.Entry(doctordb).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(doctor);
    }


}



