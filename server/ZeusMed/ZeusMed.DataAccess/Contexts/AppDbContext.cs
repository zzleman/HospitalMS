using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZeusMed.Core.Entities;

namespace ZeusMed.DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<DoctorDetail> DoctorDetails { get; set; } = null!;
        public DbSet<DoctorDetail> ServiceDetail { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.AssociatedService)
                .WithMany(s => s.AssociatedDoctors)
                .HasForeignKey(d => d.AssociatedServiceId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction); // Specify 'NO ACTION' for cascade delete

            modelBuilder.Entity<Service>()
                .HasMany(s => s.AssociatedDoctors)
                .WithOne(d => d.AssociatedService)
                .HasForeignKey(d => d.AssociatedServiceId)
                .IsRequired(false); // This should be set to false, as it is the "one" side of the relationship

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.DoctorDetail)
                .WithOne(dd => dd.Doctor)
                .HasForeignKey<DoctorDetail>(dd => dd.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceDetail)
                .WithOne(sd => sd.Service)
                .HasForeignKey<ServiceDetail>(sd => sd.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction); // Specify 'NO ACTION' for cascade delete

            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceDetail)
                .WithOne(sd => sd.Service)
                .OnDelete(DeleteBehavior.Cascade); // Specify 'CASCADE' if needed

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(false); // Optional if an appointment doesn't always have a doctor

        }

    }
}
