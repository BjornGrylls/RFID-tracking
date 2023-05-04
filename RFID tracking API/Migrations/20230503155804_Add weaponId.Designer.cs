﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RFID_tracking_API.Data;

#nullable disable

namespace RFID_tracking_API.Migrations
{
    [DbContext(typeof(RFIDTrackingDbContext))]
    [Migration("20230503155804_Add weaponId")]
    partial class AddweaponId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RFID_tracking_API.Data.Director", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LoanEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LoanStart")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ShooterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WeaponId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShooterId");

                    b.HasIndex("WeaponId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Police", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Police");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.RegularUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WeaponId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WeaponId");

                    b.ToTable("RegularUsers");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Shooter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DirectorAcceptedPictureIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPictureIdShown")
                        .HasColumnType("bit");

                    b.Property<string>("MailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShooterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DirectorAcceptedPictureIdId");

                    b.ToTable("Shooters");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Weapon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FriendlyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RfidTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Loan", b =>
                {
                    b.HasOne("RFID_tracking_API.Data.Shooter", "Shooter")
                        .WithMany()
                        .HasForeignKey("ShooterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RFID_tracking_API.Data.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shooter");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.RegularUsers", b =>
                {
                    b.HasOne("RFID_tracking_API.Data.Shooter", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RFID_tracking_API.Data.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("RFID_tracking_API.Data.Shooter", b =>
                {
                    b.HasOne("RFID_tracking_API.Data.Director", "DirectorAcceptedPictureId")
                        .WithMany()
                        .HasForeignKey("DirectorAcceptedPictureIdId");

                    b.Navigation("DirectorAcceptedPictureId");
                });
#pragma warning restore 612, 618
        }
    }
}
