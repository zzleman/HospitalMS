using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;

namespace ZeusMed.DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<DoctorDetail> DoctorDetails { get; set; } = null!;
        public DbSet<DoctorDetail> ServiceDetail { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.DoctorDetail)
                .WithOne(dd => dd.Doctor)
                .HasForeignKey<DoctorDetail>(dd => dd.Id)
                .IsRequired(false);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.DoctorDetail)
                .WithOne(dd => dd.Doctor)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceDetail)
                .WithOne(sd => sd.Service)
                .HasForeignKey<ServiceDetail>(sd => sd.Id)
                .IsRequired(false);

             modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceDetail)
                .WithOne(sd => sd.Service)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
