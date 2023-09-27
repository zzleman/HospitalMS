using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZeusMed.Core.Entities;
using ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.AuthViewModel;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.Controllers;
[Area("ZeusMedAdmin")]
public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register(RegisterVM newUser)
    {
        if (!ModelState.IsValid)
        {
            return View(newUser);
        }
        AppUser user = new()
        {
            Fullname = newUser.Fullname,
            UserName = newUser.Username,
            Email = newUser.Email
        };
        IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(newUser);
        }
        return Ok();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        AppUser user = await _userManager.FindByEmailAsync(login.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "User not found.");
            return View(login);
        }

        Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
        if (signInResult.IsLockedOut)
        {
            ModelState.AddModelError("", "Your account is locked. Please try again later.");
            return View(login);
        }
        else if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(login);
        }

        return RedirectToAction("Create", "Appointment");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home", new { area = string.Empty });
    }

}

