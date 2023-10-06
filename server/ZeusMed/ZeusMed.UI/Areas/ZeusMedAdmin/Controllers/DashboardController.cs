using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DashboardViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers
{
    [Area("ZeusMedAdmin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int doctorCount = await _context.Doctors.CountAsync();
            int serviceCount = await _context.Services.CountAsync();
            int donorCount = await _context.Donors.CountAsync();
            int birthCount = await _context.Births.CountAsync();
            int deathCount = await _context.Births.CountAsync();

            var dashboardViewModel = new DashboardVM
            {
                DoctorCount = doctorCount,
                ServiceCount = serviceCount,
                DonorCount = donorCount,
                BirthCount = birthCount,
                DeathCount = deathCount,
            };

            return View(dashboardViewModel);
        }
    }
}
