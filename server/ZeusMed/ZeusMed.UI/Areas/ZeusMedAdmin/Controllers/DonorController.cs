using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DonorViewModel;
using ZeusMed.DataAccess.Contexts;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Authorize(Roles = "Admin")]

[Area("ZeusMedAdmin")]
public class DonorController : Controller
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public DonorController(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Donor> donors = await _context.Donors.ToListAsync();

        return View(donors);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DonorVM donorViewModel)
    {
        if (ModelState.IsValid)
        {
            var donor = _mapper.Map<Donor>(donorViewModel);
            _context.Donors.Add(donor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(donorViewModel);
    }
}
