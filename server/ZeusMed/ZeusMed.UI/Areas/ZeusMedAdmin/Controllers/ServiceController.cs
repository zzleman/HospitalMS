
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.ServiceViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
public class ServiceController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ServiceController> _logger;

    public ServiceController(AppDbContext context, IMapper mapper, ILogger<ServiceController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var servicesWithDetails = await _context.Services
            .Include(s => s.ServiceDetail)
            .ToListAsync();

        return View(servicesWithDetails);
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
            Service service = _mapper.Map<Service>(servicePost);

            if (servicePost.Image != null)
            {
                string originalFileName = Path.GetFileName(servicePost.Image.FileName);
                string imageDirectory = Path.Combine("assets", "img");
                string imagePath = Path.Combine(imageDirectory, originalFileName);

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await servicePost.Image.CopyToAsync(stream);
                }

                service.ImagePath = Path.Combine("/assets/img", originalFileName);
            }

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View();
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAllServices()
    {
        var services = await _context.Services.ToListAsync();
        _context.Services.RemoveRange(services);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Update(int Id)
    {
        Service? servicedb = await _context.Services
            .Include(s => s.ServiceDetail)
            .FirstOrDefaultAsync(s => s.Id == Id);

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
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Service existingService = await _context.Services
                .Include(s => s.ServiceDetail)
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (existingService == null)
            {
                return NotFound();
            }

            existingService.Title = service.Title;
            existingService.Description = service.Description;


            if (service.ImagePath != null)
            {
                existingService.ImagePath = service.ImagePath;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(service);
    }



}

