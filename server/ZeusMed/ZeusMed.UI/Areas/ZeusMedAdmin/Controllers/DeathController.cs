using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DeathViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data;

[Authorize(Roles = "Admin")]
[Area("ZeusMedAdmin")]
public class DeathController : Controller
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public DeathController(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Death> deaths = await _context.Deaths
            .Include(b => b.Doctor)
            .ToListAsync();

        return View(deaths);
    }


    public IActionResult Create()
    {
        var viewModel = new DeathPostVM();

        ViewBag.DoctorList = _context.Doctors
            .Where(d => d.AssociatedService.Title == "Pathology")
            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Fullname })
            .ToList();

        return View(viewModel);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DeathPostVM deathPost)
    {
        if (!ModelState.IsValid)
        {
            var death = _mapper.Map<Death>(deathPost);

            var selectedDoctorId = deathPost.Doctor.Id;
            var selectedDoctor = _context.Doctors.FirstOrDefault(d => d.Id == selectedDoctorId);
            death.Doctor = selectedDoctor;

            _context.Deaths.Add(death);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        var pathologistDoctors = _context.Doctors.ToList();
        ViewBag.DoctorList = new SelectList(pathologistDoctors, "Id", "Fullname");


        return View(deathPost);
    }
}
