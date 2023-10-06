using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AppointmentViewModel;
using ZeusMed.UI.ViewModels;
using Microsoft.AspNetCore.Localization;

namespace ZeusMed.UI.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private IStringLocalizer<HomeController> _localizer;

    public HomeController(AppDbContext context, IStringLocalizer<HomeController> localizer)
    {
        _context = context;
        _localizer = localizer;
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

    [HttpPost]
    public IActionResult SetLanguage(string culture,string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(requestCulture:new RequestCulture(culture)),
            options:new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(years:1)}
            );

        return LocalRedirect(returnUrl);
    }

    [Route("/StatusCodeError/{statusCode}")]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            ViewBag.ErrorMessage = "404 Page Not Found :(";
        }
        return View("Error");
    }

    public IActionResult Success(AppointmentVM appointment)
    {
        return View(appointment);
    }

}

