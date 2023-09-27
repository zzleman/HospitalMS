using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.ViewModels;

namespace ZeusMed.UI.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM homeVM = new()
        {
            Services = await _context.Services.ToListAsync(),
            Doctors = await _context.Doctors.ToListAsync(),
        };
        return View(homeVM);
    }

    //[Route("/StatusCodeError/{statusCode}")]
    //public IActionResult Error(int statusCode)
    //{
    //    if (statusCode == 404)
    //    {
    //        ViewBag.ErrorMessage = "404 Page Not Found :(";
    //    }
    //    return View("Error");
    //}

}

