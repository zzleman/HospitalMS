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
        public DbSet<Donor> Donors { get; set; } = null!;
        public DbSet<Birth> Births { get; set; } = null!;
        public DbSet<Death> Deaths { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.AssociatedService)
                .WithMany(s => s.AssociatedDoctors)
                .HasForeignKey(d => d.AssociatedServiceId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Service>()
                .HasMany(s => s.AssociatedDoctors)
                .WithOne(d => d.AssociatedService)
                .HasForeignKey(d => d.AssociatedServiceId)
                .IsRequired(false);

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
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceDetail)
                .WithOne(sd => sd.Service)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(false);

            modelBuilder.Entity<Doctor>()
                     .HasMany(d => d.Births)
                     .WithOne(b => b.Doctor)
                     .HasForeignKey(b => b.DoctorId)
                     .IsRequired(false);

            modelBuilder.Entity<Birth>()
                .HasOne(b => b.Doctor)
                .WithMany(d => d.Births)
                .HasForeignKey(b => b.DoctorId) 
                .IsRequired(false);

            modelBuilder.Entity<Doctor>()
                    .HasMany(d => d.Deaths)
                    .WithOne(b => b.Doctor)
                    .HasForeignKey(b => b.DoctorId)
                    .IsRequired(false);

            modelBuilder.Entity<Death>()
                .HasOne(b => b.Doctor)
                .WithMany(d => d.Deaths)
                .HasForeignKey(b => b.DoctorId)
                .IsRequired(false);

        }

    }
}
