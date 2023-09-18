using Microsoft.AspNetCore.Mvc;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

