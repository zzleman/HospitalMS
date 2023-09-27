using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;

[Area("ZeusMedAdmin")]
[Authorize(Roles ="Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
