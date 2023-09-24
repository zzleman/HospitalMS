﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeusMed.DataAccess.Contexts;

#nullable disable

namespace ZeusMed.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230924070630_AddChangesTrDetail")]
    partial class AddChangesTrDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ZeusMed.Core.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssociatedServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssociatedServiceId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.DoctorDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DoctorDetail");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.ServiceDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServiceDetail");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.Doctor", b =>
                {
                    b.HasOne("ZeusMed.Core.Entities.Service", "AssociatedService")
                        .WithMany("AssociatedDoctors")
                        .HasForeignKey("AssociatedServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssociatedService");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.DoctorDetail", b =>
                {
                    b.HasOne("ZeusMed.Core.Entities.Doctor", "Doctor")
                        .WithOne("DoctorDetail")
                        .HasForeignKey("ZeusMed.Core.Entities.DoctorDetail", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.ServiceDetail", b =>
                {
                    b.HasOne("ZeusMed.Core.Entities.Service", "Service")
                        .WithOne("ServiceDetail")
                        .HasForeignKey("ZeusMed.Core.Entities.ServiceDetail", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Service");
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.Doctor", b =>
                {
                    b.Navigation("DoctorDetail")
                        .IsRequired();
                });

            modelBuilder.Entity("ZeusMed.Core.Entities.Service", b =>
                {
                    b.Navigation("AssociatedDoctors");

                    b.Navigation("ServiceDetail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}