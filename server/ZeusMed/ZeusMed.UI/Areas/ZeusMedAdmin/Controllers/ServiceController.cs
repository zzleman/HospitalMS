
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
        Service? servicedb = await _context.Services.FindAsync(Id);
        if (servicedb == null)
        {
            return NotFound();
        }
        return View(servicedb);
    }

    [HttpPost]
    [ActionName("Delete")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeletePost(int Id)
    {
        Service? servicedb = await _context.Services.FindAsync(Id);
        if (servicedb == null)
        {
            return NotFound();
        }
        _context.Services.Remove(servicedb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int Id)
    {
        Service? servicedb = await _context.Services.FindAsync(Id);
        if (servicedb == null)
        {
            return NotFound();
        }
        return View(servicedb);
    }


    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(int Id, Service service)
    {
        if (Id != service.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return View(service);
        }
        Service? servicedb = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
        if (servicedb == null)
        {
            return NotFound();
        }
        _context.Entry<Service>(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}

