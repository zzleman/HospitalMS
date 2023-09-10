using Microsoft.AspNetCore.Mvc;


namespace ZeusMed.UI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

