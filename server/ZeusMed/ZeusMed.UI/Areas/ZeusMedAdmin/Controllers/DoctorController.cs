
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
public class DoctorController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DoctorController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _context.Doctors.Include(d => d.AssociatedService).ToListAsync();
        return View(doctors);
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
        if (!ModelState.IsValid)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorPost);

            doctor.AssociatedService = _context.Services.Find(doctorPost.AssociatedServiceId);

            await _context.Doctors.AddAsync(doctor);
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

    public async Task<IActionResult> Update(int Id)
    {
        Doctor doctor = await _context.Doctors.FindAsync(Id);
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
            var doctordb = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == Id);

            if (doctordb == null)
            {
                return NotFound();
            }

            doctordb.Fullname = doctor.Fullname;
            doctordb.ImagePath = doctor.ImagePath;

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

