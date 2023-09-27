﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;
using ZeusMed.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
{
    identityOptions.User.RequireUniqueEmail = true;
    identityOptions.Password.RequireNonAlphanumeric = true;
    identityOptions.Password.RequiredLength = 8;
    identityOptions.Password.RequireDigit = true;
    identityOptions.Password.RequireLowercase = true;
    identityOptions.Password.RequireUppercase = true;

    identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    identityOptions.Lockout.MaxFailedAccessAttempts = 3;
    identityOptions.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    DeveloperExceptionPageOptions developerExceptionPageOptions = new();
    developerExceptionPageOptions.SourceCodeLineCount = 1;
    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStatusCodePagesWithRedirects("/StatusCodeError/{0}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{Id?}"
);

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{Id?}"
);

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>(); // Use UserManager<AppUser>
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string email = "doctor@doctor.com";
    string password = "Doctor123!";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new AppUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);

        if (!await roleManager.RoleExistsAsync("Doctor"))
        {
            await roleManager.CreateAsync(new IdentityRole("Doctor"));
        }

        await userManager.AddToRoleAsync(user, "Doctor");
    }
}

app.Run();
