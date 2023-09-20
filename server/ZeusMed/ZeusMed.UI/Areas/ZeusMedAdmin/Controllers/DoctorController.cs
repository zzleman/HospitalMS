using Microsoft.AspNetCore.Mvc;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
public class DoctorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

