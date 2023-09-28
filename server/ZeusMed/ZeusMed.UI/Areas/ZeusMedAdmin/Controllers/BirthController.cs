using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.BirthViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data;

[Authorize(Roles = "Admin")]
[Area("ZeusMedAdmin")]
public class BirthController : Controller
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public BirthController(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Birth> births = await _context.Births
            .Include(b => b.Doctor)
            .ToListAsync();

        return View(births);
    }


    public IActionResult Create()
    {
        var viewModel = new BirthPostVM();

        ViewBag.DoctorList = _context.Doctors
            .Where(d => d.AssociatedService.Title == "Newborn Intensive Care")
            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Fullname })
            .ToList();

        return View(viewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BirthPostVM birthViewModel)
    {
        if (!ModelState.IsValid)
        {
            var birth = _mapper.Map<Birth>(birthViewModel);

            var selectedDoctorId = birthViewModel.Doctor.Id;
            var selectedDoctor = _context.Doctors.FirstOrDefault(d => d.Id == selectedDoctorId);
            birth.Doctor = selectedDoctor;

            _context.Births.Add(birth);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        var doctors = _context.Doctors
            .ToList();

        ViewBag.DoctorList = new SelectList(doctors, "Id", "Fullname");

        return View(birthViewModel);
    }

}
