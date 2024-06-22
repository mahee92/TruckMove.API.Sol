﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TruckMove.API.DAL.Models;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    [DbContext(typeof(TrukMoveContext))]
    partial class TrukMoveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
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

                    b.Property<string>("CompanyABN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("Controller")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Driver")
                        .HasColumnType("int");

                    b.Property<string>("DropOfCoordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DropOfLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("EstimatedDaysofTravel")
                        .HasColumnType("float");

                    b.Property<DateTime?>("EstimatedDeliveryDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PickupCoordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PickupDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PickupLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<double?>("TotalDistance")
                        .HasColumnType("float");

                    b.Property<double?>("TotalDrivingTime")
                        .HasColumnType("float");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Driver");

                    b.HasIndex("Status");

                    b.HasIndex(new[] { "CompanyId" }, "IX_Jobs_CompanyId");

                    b.HasIndex(new[] { "Controller" }, "IX_Jobs_Controller");

                    b.HasIndex(new[] { "CreatedById" }, "IX_Jobs_CreatedById");

                    b.HasIndex(new[] { "UpdatedById" }, "IX_Jobs_UpdatedById");

                    b.HasIndex(new[] { "VehicleId" }, "UQ_Jobs_VehicleId")
                        .IsUnique()
                        .HasFilter("[VehicleId] IS NOT NULL");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.JobContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.HasIndex(new[] { "ContactId" }, "IX_JobContacts_ContactId");

                    b.HasIndex(new[] { "JobId" }, "IX_JobContacts_JobId");

                    b.ToTable("JobContacts");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.JobSequence", b =>
                {
                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.ToTable("JobSequence", (string)null);
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.JobStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("JobStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A job that has been created in the system but does not have the minimum required information to complete the booking",
                            Status = "Planned"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A job that has the minimum required information (pickup location, dropoff location, vehicle information, assigned driver)",
                            Status = "Booked"
                        },
                        new
                        {
                            Id = 3,
                            Description = "A booked job that is on or passed the pickup date.",
                            Status = "ReadyForPickup"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Status once the driver has arrived to pick up the truck and is done the pre departure check",
                            Status = "PreDepartureChecked"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Driver has completed the acknowledgement ",
                            Status = "Acknowledged"
                        },
                        new
                        {
                            Id = 7,
                            Description = "A job that is currently in progress",
                            Status = "InProgress"
                        },
                        new
                        {
                            Id = 8,
                            Description = "status when driver stops for the night",
                            Status = "Stopped"
                        },
                        new
                        {
                            Id = 9,
                            Description = "status when driver stops for the night",
                            Status = "Delayed"
                        },
                        new
                        {
                            Id = 10,
                            Description = "A job that has arrived at the destination",
                            Status = "Arrived"
                        },
                        new
                        {
                            Id = 11,
                            Description = "status when driver is competed arrival checklist",
                            Status = "ArrivalChecked"
                        },
                        new
                        {
                            Id = 12,
                            Description = "QA completed",
                            Status = "QADone"
                        },
                        new
                        {
                            Id = 13,
                            Description = "Payment Done",
                            Status = "PaymentDone"
                        },
                        new
                        {
                            Id = 14,
                            Description = "Billing Done",
                            Status = "BillingDone"
                        },
                        new
                        {
                            Id = 15,
                            Description = "A job that has been completed successfully",
                            Status = "Completed"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "OpsManager"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "AdminTeam"
                        },
                        new
                        {
                            Id = 4,
                            RoleName = "PayrollTeam"
                        },
                        new
                        {
                            Id = 5,
                            RoleName = "Driver"
                        });
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

            modelBuilder.Entity("TruckMove.API.DAL.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Colour")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(1)))");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<string>("Model")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<string>("Rego")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<string>("Vin")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .HasColumnName("VIN")
                        .IsFixedLength();

                    b.Property<string>("Year")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.HasIndex(new[] { "JobId" }, "UQ_Vehicles_JobId")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.VehicleImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleImages");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.VehicleNote", b =>
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

                    b.Property<bool?>("IsVisibleToDriver")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleNotes");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.WayPoint", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("JobId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("WayPoints");
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

                    b.HasOne("TruckMove.API.DAL.Models.User", "DriverNavigation")
                        .WithMany("JobDriverNavigations")
                        .HasForeignKey("Driver")
                        .HasConstraintName("FK_Jobs_Users");

                    b.HasOne("TruckMove.API.DAL.Models.JobStatus", "StatusNavigation")
                        .WithMany("Jobs")
                        .HasForeignKey("Status")
                        .HasConstraintName("FK_Jobs_JobStatus");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany("JobUpdatedBies")
                        .HasForeignKey("UpdatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Vehicle", "Vehicle")
                        .WithOne("Job")
                        .HasForeignKey("TruckMove.API.DAL.Models.Job", "VehicleId")
                        .HasConstraintName("FK_Jobs_Vehicles");

                    b.Navigation("Company");

                    b.Navigation("ControllerNavigation");

                    b.Navigation("CreatedBy");

                    b.Navigation("DriverNavigation");

                    b.Navigation("StatusNavigation");

                    b.Navigation("UpdatedBy");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.JobContact", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.Contact", "Contact")
                        .WithMany("JobContacts")
                        .HasForeignKey("ContactId")
                        .IsRequired()
                        .HasConstraintName("FK_JobContacts_Contacts");

                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Job", "Job")
                        .WithMany("JobContacts")
                        .HasForeignKey("JobId")
                        .IsRequired()
                        .HasConstraintName("FK_JobContacts_Jobs");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Contact");

                    b.Navigation("CreatedBy");

                    b.Navigation("Job");

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

            modelBuilder.Entity("TruckMove.API.DAL.Models.Vehicle", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Job", "JobNavigation")
                        .WithOne("VehicleNavigation")
                        .HasForeignKey("TruckMove.API.DAL.Models.Vehicle", "JobId")
                        .IsRequired();

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("JobNavigation");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.VehicleImage", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Vehicle", "Vehicle")
                        .WithMany("VehicleImages")
                        .HasForeignKey("VehicleId")
                        .IsRequired()
                        .HasConstraintName("FK_VehicleImages_Vehicles");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.VehicleNote", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Vehicle", "Vehicle")
                        .WithMany("VehicleNotes")
                        .HasForeignKey("VehicleId")
                        .IsRequired()
                        .HasConstraintName("FK_VehicleNotes_Vehicles");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.WayPoint", b =>
                {
                    b.HasOne("TruckMove.API.DAL.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TruckMove.API.DAL.Models.Job", "Job")
                        .WithMany("WayPoints")
                        .HasForeignKey("JobId")
                        .IsRequired()
                        .HasConstraintName("FK_WayPoints_Jobs");

                    b.HasOne("TruckMove.API.DAL.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Job");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Company", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Contact", b =>
                {
                    b.Navigation("JobContacts");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Job", b =>
                {
                    b.Navigation("JobContacts");

                    b.Navigation("VehicleNavigation");

                    b.Navigation("WayPoints");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.JobStatus", b =>
                {
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

                    b.Navigation("JobDriverNavigations");

                    b.Navigation("JobUpdatedBies");

                    b.Navigation("UserRoleCreatedBies");

                    b.Navigation("UserRoleUpdatedBies");

                    b.Navigation("UserRoleUsers");
                });

            modelBuilder.Entity("TruckMove.API.DAL.Models.Vehicle", b =>
                {
                    b.Navigation("Job");

                    b.Navigation("VehicleImages");

                    b.Navigation("VehicleNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
