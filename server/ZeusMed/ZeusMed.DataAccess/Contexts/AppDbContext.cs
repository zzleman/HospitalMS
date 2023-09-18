using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;

namespace ZeusMed.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options){}

    public DbSet<Service> Services { get; set; } = null!;

    public DbSet<Doctor> Doctors { get; set; } = null!;

}

