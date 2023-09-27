using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;
using ZeusMed.Core.Entities;
using ZeusMed.UI.ViewModels;

namespace ZeusMed.UI.Controllers
{
    public class ServiceDetailController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceDetailController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            // Fetch the service details from the database using the provided ID
            Service service = _context.Services.Include(s => s.ServiceDetail).FirstOrDefault(s => s.Id == id);

            if (service == null)
            {
                return NotFound(); // Handle when the service is not found
            }

            var serviceVM = new ServiceVM
            {
                Title = service.Title,
                Description = service.Description,
                ImagePath = service.ImagePath
            };

            return View(serviceVM);
        }
    }
}
