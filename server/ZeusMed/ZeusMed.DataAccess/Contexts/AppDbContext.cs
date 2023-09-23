using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;

namespace ZeusMed.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options){}

    public DbSet<Service> Services { get; set; } = null!;

    public DbSet<Doctor> Doctors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.AssociatedService)
            .WithMany(s => s.AssociatedDoctors)
            .HasForeignKey(d => d.AssociatedServiceId)
            .IsRequired(false);

        modelBuilder.Entity<Service>()
            .HasMany(s => s.AssociatedDoctors)
            .WithOne(d => d.AssociatedService)
            .HasForeignKey(d => d.AssociatedServiceId)
            .IsRequired();
    }

}

