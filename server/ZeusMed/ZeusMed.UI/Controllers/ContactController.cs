using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.ViewModels;

namespace ZeusMed.UI.Controllers;

public class ContactController : Controller
{
    private readonly AppDbContext _context;

    public ContactController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

}




