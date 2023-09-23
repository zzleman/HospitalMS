
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.ServiceViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
public class ServiceController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ServiceController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Doctors.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(DoctorPostVM doctorPost)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Doctor doctor = _mapper.Map<Doctor>(doctorPost);
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}

