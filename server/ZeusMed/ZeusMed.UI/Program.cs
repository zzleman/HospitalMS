using Microsoft.EntityFrameworkCore;
using ZeusMed.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{Id?}"
);

app.MapControllerRoute(
    name:"Default",
    pattern:"{controller=Home}/{action=Index}/{Id?}"
);

app.Run();

