using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.UI.ViewModels;
using System.Linq;

namespace ZeusMed.UI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var serviceVM = new ServiceVM
            {
                Services = _context.Services.ToList()
            };
            return View(serviceVM);
        }

        public IActionResult Details(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);

            if (service == null)
            {
                return NotFound(); 
            }

            var serviceVM = new ServiceVM
            {
                Title = service.Title,
                Description = service.Description,
                Info = service.ServiceDetail.Info,
                ImagePath = service.ImagePath
            };

            return View(serviceVM);
        }
    }
}
