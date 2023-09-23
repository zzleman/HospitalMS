using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.ViewModels;

namespace ZeusMed.UI.Controllers;

public class AboutController : Controller
{
    private readonly AppDbContext _context;

    public AboutController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {

        var aboutVM = new AboutVM
        {
            Title = "About Us",
        };

        return View(aboutVM);
    }

}

