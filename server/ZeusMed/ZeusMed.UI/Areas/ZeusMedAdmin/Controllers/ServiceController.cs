
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
        return View(await _context.Services.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(ServicePostVM servicePost)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Service service = _mapper.Map<Service>(servicePost);
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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

}

