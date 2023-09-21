using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
public class DoctorController : Controller
{
    private readonly AppDbContext _context;

    public DoctorController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Doctors.ToListAsync());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }
}

