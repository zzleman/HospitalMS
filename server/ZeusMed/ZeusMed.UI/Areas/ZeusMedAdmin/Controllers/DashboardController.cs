using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;

[Area("ZeusMedAdmin")]
[Authorize(Roles ="Admin,Doctor")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
}
