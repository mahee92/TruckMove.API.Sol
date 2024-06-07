﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TruckMove.API.DAL.Models;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    [DbContext(typeof(TrukMoveContext))]
    [Migration("20240607153728_2024-6-7-1")]
    partial class _2024671
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence<int>("JobSeq")
                .StartsAt(2475L);

            modelBuilder.Entity("TruckMove.API.DAL.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountsEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CompanyAbn")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("CompanyABN");

                    b.Property<string>("CompanyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CompanyStreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PrimaryEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CreatedById" }, "IX_Companies_CreatedById");

                    b.HasIndex(new[] { "UpdatedById" }, "IX_Companies_UpdatedById");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ContactLandline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactMobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactStreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactsEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CompanyId" }, "IX_Contacts_CompanyId");

                    b.HasIndex(new[] { "CreatedById" }, "IX_Contacts_CreatedById");

                    b.HasIndex(new[] { "UpdatedById" }, "IX_Contacts_UpdatedById");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("Controller")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DropOfLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CompanyId" }, "IX_Jobs_CompanyId");

                    b.HasIndex(new[] { "Controller" }, "IX_Jobs_Controller");

                    b.HasIndex(new[] { "CreatedById" }, "IX_Jobs_CreatedById");

                    b.HasIndex(new[] { "UpdatedById" }, "IX_Jobs_UpdatedById");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.JobSequence", b =>
                {
                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.ToTable("JobSequence", (string)null);
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CreatedById" }, "IX_Users_CreatedById");

                    b.HasIndex(new[] { "UpdatedById" }, "IX_Users_UpdatedById");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CreatedById" }, "IX_UserRole_CreatedById");

                    b.HasIndex(new[] { "RoleId" }, "IX_UserRole_RoleId");

                    b.HasIndex(new[] { "UpdatedById" }, "IX_UserRole_UpdatedById");

                    b.HasIndex(new[] { "UserId" }, "IX_UserRole_UserId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Company", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany("CompanyCreatedBies")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany("CompanyUpdatedBies")
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Contact", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany("ContactCreatedBies")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany("ContactUpdatedBies")
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Company");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Job", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.Company", "Company")
                        .WithMany("Jobs")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TruckMove.API.DAL.Models.User", "ControllerNavigation")
                        .WithMany("JobControllerNavigations")
                        .HasForeignKey("Controller");

                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany("JobCreatedBies")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany("JobUpdatedBies")
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Company");

                    b.Navigation("ControllerNavigation");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.User", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany("InverseCreatedBy")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany("InverseUpdatedBy")
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.UserRole", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany("UserRoleCreatedBies")
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany("UserRoleUpdatedBies")
                        .HasForeignKey("UpdatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "User")
                        .WithMany("UserRoleUsers")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Role");

                    b.Navigation("UpdatedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Company", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.User", b =>
                {
                    b.Navigation("CompanyCreatedBies");

                    b.Navigation("CompanyUpdatedBies");

                    b.Navigation("ContactCreatedBies");

                    b.Navigation("ContactUpdatedBies");

                    b.Navigation("InverseCreatedBy");

                    b.Navigation("InverseUpdatedBy");

                    b.Navigation("JobControllerNavigations");

                    b.Navigation("JobCreatedBies");

                    b.Navigation("JobUpdatedBies");

                    b.Navigation("UserRoleCreatedBies");

                    b.Navigation("UserRoleUpdatedBies");

                    b.Navigation("UserRoleUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
