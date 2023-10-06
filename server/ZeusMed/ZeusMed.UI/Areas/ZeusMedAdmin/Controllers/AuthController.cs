
using System.Net;
using System.Net.Mail;
using Business.Interfaces;
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
    private readonly IMailService _mailService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<Core.Entities.AppUser> signInManager, IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mailService = mailService;
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
        ZeusMed.Core.Entities.AppUser user = new()
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
        return View("Login");
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

        ZeusMed.Core.Entities.AppUser user = await _userManager.FindByEmailAsync(login.Email);
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

        if (User.IsInRole("Admin,Doctor"))
        {
            return RedirectToAction("Index", "Dashboard");
        }
        else
        {
            return RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home", new { area = string.Empty });
    }


    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
    {
        if (!ModelState.IsValid) return View(forgotPasswordVM);

        var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);

        if (user is null)
        {
            ModelState.AddModelError("Email", "Not found");
            return View(forgotPasswordVM);
        }


        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = Url.Action(nameof(ResetPassword), "Auth",  new { token, userId = user.Id }, Request.Scheme);

        return Content(link);

    }

    public async Task<IActionResult> ResetPassword(string userId, string token)
    {

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM, string userId, string token)
    {

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid) return View(resetPasswordVM);

        var identityResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.NewPassword);

        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordVM);
        }

        return RedirectToAction(nameof(Login));
    }

    //public IActionResult SendEmail()
    //{
    //    return View();
    //}
    //[HttpPost]
    //public IActionResult SendEmail(string to, string subject, string body)
    //{
    //    try
    //    {
    //        Thread email = new(delegate ()
    //        {
    //            Sendmail.Email(to, subject, body);
    //        });
    //        email.IsBackground = true;
    //        email.Start();
    //        TempData["alert"] = "Email Sucessfully Sent";
    //    }
    //    catch (Exception)
    //    {
    //        TempData["alert"] = "Problem Sending mail please check the configuration";

    //    }
    //    return Redirect("Home/Index");
    //}
}
