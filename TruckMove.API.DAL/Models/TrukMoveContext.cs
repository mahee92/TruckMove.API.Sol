using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.DAL.Models
{
    public partial class TrukMoveContext : DbContext
    {
        public TrukMoveContext()
        {
        }

        public TrukMoveContext(DbContextOptions<TrukMoveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accommodation> Accommodations { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobContact> JobContacts { get; set; } = null!;
        public virtual DbSet<JobSequence> JobSequences { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
      
        public virtual DbSet<WayPoint> WayPoints { get; set; } = null!;
        public virtual DbSet<JobStatus> JobStatuses { get; set; } = null!;

        public virtual DbSet<PreDepartureChecklist> PreDepartureChecklists { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;

        public virtual DbSet<Image> Images { get; set; } = null!;

        public virtual DbSet<Leg> Legs { get; set; } = null!;
        public virtual DbSet<LegStatus> LegStatuses { get; set; } = null!;

        public virtual DbSet<Acknowledgement> Acknowledgements { get; set; } = null!;

        public virtual DbSet<HookupType> HookupTypes { get; set; } = null!;
        public virtual DbSet<Trailer> Trailers { get; set; } = null!;

        public virtual DbSet<PermitsAndPlate> PermitsAndPlates { get; set; } = null!;

        public virtual DbSet<TaskStatus> TaskStatuses { get; set; } = null!;

        public virtual DbSet<Variance> Variances { get; set; } = null!;

      

        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<PublicTransport> PublicTransports { get; set; } = null!;
        public virtual DbSet<PublicTransportType> PublicTransportTypes { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
              // optionsBuilder.UseSqlServer("Server=10.111.111.23;Database=TruckMove-DevDB;User Id=dev1;Password=hfjdhfkjkdsfd787*Fg;");
                optionsBuilder.UseSqlServer("Server=(localdb)\\localdbtest;Database=TrukMove-18;Trusted_Connection=True;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
          new Role { Id = (int)RoleEnum.Administrator, RoleName = RoleEnum.Administrator.ToString() },
          new Role { Id = (int)RoleEnum.OpsManager, RoleName = RoleEnum.OpsManager.ToString() },
          new Role { Id = (int)RoleEnum.AdminTeam, RoleName = RoleEnum.AdminTeam.ToString() },
          new Role { Id = (int)RoleEnum.PayrollTeam, RoleName = RoleEnum.PayrollTeam.ToString() },
          new Role { Id = (int)RoleEnum.Driver, RoleName = RoleEnum.Driver.ToString() }
          );
            modelBuilder.Entity<JobStatus>().HasData(
                   new JobStatus { Id = (int)JobStatusEnum.Planned, Status = JobStatusEnum.Planned.ToString(), Description = "A job that has been created in the system but does not have the minimum required information to complete the booking" },
                   new JobStatus { Id = (int)JobStatusEnum.Booked, Status = JobStatusEnum.Booked.ToString(), Description = "A job that has the minimum required information (pickup location, dropoff location, vehicle information, assigned driver)" },
                   new JobStatus { Id = (int)JobStatusEnum.ReadyForPickup, Status = JobStatusEnum.ReadyForPickup.ToString(), Description = "A booked job that is on or passed the pickup date." },
                   new JobStatus { Id = (int)JobStatusEnum.PreDepartureChecked, Status = JobStatusEnum.PreDepartureChecked.ToString(), Description = "Status once the driver has arrived to pick up the truck and is done the pre departure check" },
                   new JobStatus { Id = (int)JobStatusEnum.Acknowledged, Status = JobStatusEnum.Acknowledged.ToString(), Description = "Driver has completed the acknowledgement " },
                   new JobStatus { Id = (int)JobStatusEnum.InProgress, Status = JobStatusEnum.InProgress.ToString(), Description = "A job that is currently in progress" },
                   new JobStatus { Id = (int)JobStatusEnum.Stopped, Status = JobStatusEnum.Stopped.ToString(), Description = "status when driver stops for the night" },
                   new JobStatus { Id = (int)JobStatusEnum.Delayed, Status = JobStatusEnum.Delayed.ToString(), Description = "status when driver stops for the night" },
                   new JobStatus { Id = (int)JobStatusEnum.Arrived, Status = JobStatusEnum.Arrived.ToString(), Description = "A job that has arrived at the destination" },
                   new JobStatus { Id = (int)JobStatusEnum.ArrivalChecked, Status = JobStatusEnum.ArrivalChecked.ToString(), Description = "status when driver is competed arrival checklist" },
                   new JobStatus { Id = (int)JobStatusEnum.QADone, Status = JobStatusEnum.QADone.ToString(), Description = "QA completed" },
                   new JobStatus { Id = (int)JobStatusEnum.PaymentDone, Status = JobStatusEnum.PaymentDone.ToString(), Description = "Payment Done" },
                   new JobStatus { Id = (int)JobStatusEnum.BillingDone, Status = JobStatusEnum.BillingDone.ToString(), Description = "Billing Done" },
                   new JobStatus { Id = (int)JobStatusEnum.Completed, Status = JobStatusEnum.Completed.ToString(), Description = "A job that has been completed successfully" }
               );

               modelBuilder.Entity<LegStatus>().HasData(
                 new LegStatus { Id = (int)LegStatusEnum.Planned ,Status= LegStatusEnum.Planned.ToString()},
                 new LegStatus { Id = (int)LegStatusEnum.InProgress, Status = LegStatusEnum.InProgress.ToString() },
                 new LegStatus { Id = (int)LegStatusEnum.Completed , Status = LegStatusEnum.Completed.ToString() }
                 );

            modelBuilder.Entity<HookupType>().HasData(
                 new HookupType { Id = (int)HookUpTypeEnum.HU_Single,Type= HookUpTypeEnum.HU_Single.ToString(), Description = "HU Single" },
                 new HookupType { Id = (int)HookUpTypeEnum.HU_Double, Type = HookUpTypeEnum.HU_Double.ToString(), Description = "HU Double" },
                 new HookupType { Id = (int)HookUpTypeEnum.FOUR_RA, Type = HookUpTypeEnum.FOUR_RA.ToString(), Description = "4RA (4 Rigid Axle )" }
                 ); 
            modelBuilder.Entity<Variance>().HasData(
                 new Variance { Id = (int)VariancesEnum._default, Name = VariancesEnum._default.ToString(), Description = "Default" },
                 new Variance { Id = (int)VariancesEnum.DG, Name = VariancesEnum.DG.ToString(), Description = "DG" },
                 new Variance { Id = (int)VariancesEnum.Sat_rate, Name = VariancesEnum.Sat_rate.ToString(), Description = "Sat Rate" },
                 new Variance { Id = (int)VariancesEnum.Sun_rate, Name = VariancesEnum.Sun_rate.ToString(), Description = "Sun Rate" },
                 new Variance { Id = (int)VariancesEnum.G7, Name = VariancesEnum.G7.ToString(), Description = "G7" },
                 new Variance { Id = (int)VariancesEnum.Public_Holiday, Name = VariancesEnum.Public_Holiday.ToString(), Description = "Public Holiday" },
                 new Variance { Id = (int)VariancesEnum.G4, Name = VariancesEnum.G4.ToString(), Description = "G4" },
                 new Variance { Id = (int)VariancesEnum.Bookining_Bullbar, Name = VariancesEnum.Bookining_Bullbar.ToString(), Description = "Booking (Bullbar)" }
             );
            modelBuilder.Entity<TaskStatus>().HasData(
                 new TaskStatus { Id = (int)TaskStatusEnum.Planned, Status = TaskStatusEnum.Planned.ToString()},
                 new TaskStatus { Id = (int)TaskStatusEnum.InProgress, Status = TaskStatusEnum.InProgress.ToString()},
                 new TaskStatus { Id = (int)TaskStatusEnum.Completed, Status = TaskStatusEnum.Completed.ToString() }
                 );

            modelBuilder.Entity<PublicTransportType>().HasData(
                 new PublicTransportType { Id = (int)PublicTransportTypeEnum.Train, Type = PublicTransportTypeEnum.Train.ToString() },
                  new PublicTransportType { Id = (int)PublicTransportTypeEnum.Plane, Type = PublicTransportTypeEnum.Plane.ToString() },
                  new PublicTransportType { Id = (int)PublicTransportTypeEnum.Uber, Type = PublicTransportTypeEnum.Uber.ToString()  },
                  new PublicTransportType { Id = (int)PublicTransportTypeEnum.Taxi, Type = PublicTransportTypeEnum.Taxi.ToString() },
                  new PublicTransportType { Id = (int)PublicTransportTypeEnum.Other, Type = PublicTransportTypeEnum.Other.ToString() }
                 );

            modelBuilder.Entity<Accommodation>(entity =>
            {
                entity.ToTable("Accommodation");

                entity.Property(e => e.IsActive)
                      .IsRequired()
                      .HasDefaultValueSql("(CONVERT([bit],(1)))");
                entity.HasOne(d => d.CreatedBy)
                   .WithMany(p => p.AccommodationCreatedBies)
                   .HasForeignKey(d => d.CreatedById);

                entity.Property(e => e.OrganiseNow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.AccommodationUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ReferenceNumber).HasMaxLength(50);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.AccommodationAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_Accommodation_Users1");

                entity.HasOne(d => d.DriverNavigation)
                    .WithMany(p => p.AccommodationDriverNavigations)
                    .HasForeignKey(d => d.Driver)
                    .HasConstraintName("FK_Accommodation_Users");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Accommodations)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accommodation_Jobs");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Accommodations)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accommodation_TaskStatus");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Companies_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Companies_UpdatedById");

                entity.Property(e => e.AccountsEmail).HasMaxLength(100);

               

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                       .IsRequired()
                       .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PrimaryEmail).HasMaxLength(100);

                entity.HasOne(d => d.CreatedBy)
                       .WithMany(p => p.CompanyCreatedBies)
                       .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.UpdatedBy)
                       .WithMany(p => p.CompanyUpdatedBies)
                       .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_Contacts_CompanyId");

                entity.HasIndex(e => e.CreatedById, "IX_Contacts_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Contacts_UpdatedById");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.ContactCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.ContactUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Images_Jobs");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.TrailerId)
                    .HasConstraintName("FK_Images_Trailers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Images_Vehicles");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_Jobs_CompanyId");
                entity.HasIndex(e => e.Controller, "IX_Jobs_Controller");
                entity.HasIndex(e => e.CreatedById, "IX_Jobs_CreatedById");
                entity.HasIndex(e => e.UpdatedById, "IX_Jobs_UpdatedById");
                entity.HasIndex(e => e.VehicleId, "UQ_Jobs_VehicleId")
                    .IsUnique();
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("datetime");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");
                entity.Property(e => e.PickupDate).HasColumnType("datetime");
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CompanyId);
                entity.HasOne(d => d.ControllerNavigation)
                    .WithMany(p => p.JobControllerNavigations)
                    .HasForeignKey(d => d.Controller);
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.JobCreatedBies)
                    .HasForeignKey(d => d.CreatedById);
                entity.HasOne(d => d.DriverNavigation)
                    .WithMany(p => p.JobDriverNavigations)
                    .HasForeignKey(d => d.Driver)
                    .HasConstraintName("FK_Jobs_Users");
                entity.HasOne(d => d.StatusNavigation)
                   .WithMany(p => p.Jobs)
                   .HasForeignKey(d => d.Status)
                   .HasConstraintName("FK_Jobs_JobStatus");
                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.JobUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
                entity.HasOne(d => d.Vehicle)
                    .WithOne(p => p.Job)
                    .HasForeignKey<Job>(d => d.VehicleId)
                    .HasConstraintName("FK_Jobs_Vehicles");

            });
            modelBuilder.Entity<JobContact>(entity =>

            {

                entity.HasIndex(e => e.ContactId, "IX_JobContacts_ContactId");

                entity.HasIndex(e => e.JobId, "IX_JobContacts_JobId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");


                entity.HasOne(d => d.Contact)

                    .WithMany(p => p.JobContacts)

                    .HasForeignKey(d => d.ContactId)

                    .OnDelete(DeleteBehavior.ClientSetNull)

                    .HasConstraintName("FK_JobContacts_Contacts");



                entity.HasOne(d => d.Job)

                    .WithMany(p => p.JobContacts)

                    .HasForeignKey(d => d.JobId)

                    .OnDelete(DeleteBehavior.ClientSetNull)

                    .HasConstraintName("FK_JobContacts_Jobs");

            });

            modelBuilder.Entity<JobSequence>(entity =>

            {
                entity.HasNoKey();

                entity.ToTable("JobSequence");

            });

            modelBuilder.Entity<JobStatus>(entity =>

            {
                entity.ToTable("JobStatus");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.Status).HasMaxLength(50);

            });
            modelBuilder.Entity<Note>(entity =>

            {

                //entity.Property(e => e.NoteText).HasColumnName("Note");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Accommodation)
                  .WithMany(p => p.Notes)
                  .HasForeignKey(d => d.AccommodationId)
                  .HasConstraintName("FK_Notes_Accommodation");


                entity.HasOne(d => d.Job)

                    .WithMany(p => p.Notes)

                    .HasForeignKey(d => d.JobId)

                    .OnDelete(DeleteBehavior.ClientSetNull)

                    .HasConstraintName("FK_Notes_Jobs");



                entity.HasOne(d => d.PreDeparturechecklist)

                    .WithMany(p => p.Notes)

                    .HasForeignKey(d => d.PreDeparturechecklistId)

                    .HasConstraintName("FK_Notes_PreDepartureChecklist");



                entity.HasOne(d => d.Vehicle)

                    .WithMany(p => p.Notes)

                    .HasForeignKey(d => d.VehicleId)

                    .HasConstraintName("FK_Notes_Vehicles");

                entity.HasOne(d => d.Trailer)
                   .WithMany(p => p.Notes)
                   .HasForeignKey(d => d.TrailerId)
                   .HasConstraintName("FK_Notes_Trailers");

                entity.HasOne(d => d.PermitAndPlates)
                   .WithMany(p => p.Notes)
                   .HasForeignKey(d => d.PermitAndPlatesId)
                   .HasConstraintName("FK_Notes_PermitsAndPlates");

                entity.HasOne(d => d.PublicTransport)
                 .WithMany(p => p.Notes)
                 .HasForeignKey(d => d.PublicTransportId)
                 .HasConstraintName("FK_Notes_PublicTransport");

            });
            modelBuilder.Entity<PermitsAndPlate>(entity =>
            {
                entity.Property(e => e.IsActive)
                   .IsRequired()
                   .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.PermitNumber).HasMaxLength(50);

                entity.Property(e => e.PlateNumber).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.PermitsAndPlates)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_PermitsAndPlates_Users");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PermitsAndPlates)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermitsAndPlates_Jobs");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.PermitsAndPlates)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermitsAndPlates_TaskStatus");

                entity.HasIndex(e => e.CreatedById, "IX_PermitsAndPlate_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_PermitsAndPlate_UpdatedById");


                entity.HasOne(d => d.CreatedBy)
                   .WithMany(p => p.PermitsAndPlatesCreatedBies)
                   .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.PermitsAndPlatesUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

         
            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });
            modelBuilder.Entity<Variance>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PreDepartureChecklist>(entity =>

            {

                entity.ToTable("PreDepartureChecklist");

                entity.Property(e => e.IsActive)
                     .IsRequired()
                     .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasIndex(e => e.JobId, "UQ_PreDepartureChecklist_JobId")

                    .IsUnique();



                entity.Property(e => e.AirAndElectrics).HasMaxLength(10);



                entity.Property(e => e.AllLightsAndIndicators).HasMaxLength(10);



                entity.Property(e => e.CheckInsideTruckTrailer).HasMaxLength(10);



                entity.Property(e => e.CheckTruckHeight).HasMaxLength(10);



                entity.Property(e => e.FrontDamage).HasMaxLength(10);



                entity.Property(e => e.FuelLevel).HasColumnType("decimal(5, 2)");



                entity.Property(e => e.JackAndTools).HasMaxLength(10);



                entity.Property(e => e.KeysFobTotalKeys).HasMaxLength(10);



                entity.Property(e => e.LeftHandDamage).HasMaxLength(10);



                entity.Property(e => e.Oil).HasMaxLength(10);



                entity.Property(e => e.OwnersManual).HasMaxLength(10);



                entity.Property(e => e.RearDamage).HasMaxLength(10);



                entity.Property(e => e.RightHandDamage).HasMaxLength(10);



                entity.Property(e => e.SpareRim).HasMaxLength(10);



                entity.Property(e => e.TyresCondition).HasMaxLength(10);



                entity.Property(e => e.VehicleCleanFreeOfRubbish).HasMaxLength(10);



                entity.Property(e => e.VisuallyDipAndCheckTaps).HasMaxLength(10);



                entity.Property(e => e.Water).HasMaxLength(10);



                entity.Property(e => e.WindscreenDamageWipers).HasMaxLength(10);



                entity.HasOne(d => d.Job)

                    .WithOne(p => p.PreDepartureChecklist)

                    .HasForeignKey<PreDepartureChecklist>(d => d.JobId)

                    .OnDelete(DeleteBehavior.ClientSetNull)

                    .HasConstraintName("FK_PreDepartureChecklist_Jobs");

            });



            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Users_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Users_UpdatedById");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.InverseCreatedBy)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.InverseUpdatedBy)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasIndex(e => e.CreatedById, "IX_UserRole_CreatedById");

                entity.HasIndex(e => e.RoleId, "IX_UserRole_RoleId");

                entity.HasIndex(e => e.UpdatedById, "IX_UserRole_UpdatedById");

                entity.HasIndex(e => e.UserId, "IX_UserRole_UserId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.UserRoleCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.UserRoleUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.JobId, "UQ_Vehicles_JobId")
                .IsUnique();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");


                entity.Property(e => e.Colour)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Make)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Rego)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Vin)
                    .HasMaxLength(100)
                    .HasColumnName("VIN")
                    .IsFixedLength();

                entity.Property(e => e.Year)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.JobNavigation)
                     .WithOne(p => p.VehicleNavigation)
                     .HasForeignKey<Vehicle>(d => d.JobId)
                     .OnDelete(DeleteBehavior.ClientSetNull);
            });
         
            modelBuilder.Entity<WayPoint>(entity =>
            {
               
                entity.HasOne(d => d.Job)

                    .WithMany(p => p.WayPoints)

                    .HasForeignKey(d => d.JobId)

                    .OnDelete(DeleteBehavior.ClientSetNull)

                    .HasConstraintName("FK_WayPoints_Jobs");

            });

            modelBuilder.Entity<Leg>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

              //  entity.Property(e => e.EndLocation).HasMaxLength(500);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartLocation).HasMaxLength(500);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Legs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_Jobs");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Legs)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_LegStatus");

                entity.HasOne(d => d.VarianceNavigation)
                    .WithMany(p => p.Legs)
                    .HasForeignKey(d => d.Variance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_Variances");
            });

            modelBuilder.Entity<LegStatus>(entity =>
            {
                entity.ToTable("LegStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });
            modelBuilder.Entity<Acknowledgement>(entity =>
            {
                entity.ToTable("Acknowledgement");

                entity.HasIndex(e => e.LegId, "UQ_Acknowledge_LegId")
                    .IsUnique();
                    entity.HasOne(d => d.Job)
                    .WithMany(p => p.Acknowledgements)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Acknowledgement_Jobs");

                entity.HasOne(d => d.Leg)
                    .WithOne(p => p.Acknowledgement)
                    .HasForeignKey<Acknowledgement>(d => d.LegId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Acknowledgement_Legs");
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.Rego).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.HasOne(d => d.HookupTypeNavigation)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.HookupType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trailers_HookupTypes");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trailers_Jobs");
            });

            modelBuilder.Entity<HookupType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(200);
            });


            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.IsActive)
                 .IsRequired()
                 .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.PermitAndPlate)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.PermitAndPlateId)
                    .HasConstraintName("FK_TaskAttachments_PermitsAndPlates");

                entity.HasOne(d => d.Accommodation)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AccommodationId)
                    .HasConstraintName("FK_TaskAttachments_Accommodation");
            });
            modelBuilder.Entity<PublicTransport>(entity =>
            {
                entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.ToTable("PublicTransport");

                entity.Property(e => e.ArrivalDateTime).HasColumnType("datetime");

                entity.Property(e => e.Daterequired).HasColumnType("datetime");

                entity.Property(e => e.DepartureDateTime).HasColumnType("datetime");

                entity.Property(e => e.ReferenceNumber).HasMaxLength(100);

                entity.Property(e => e.Requiredsuburb).HasColumnName("requiredsuburb");

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.PublicTransportAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_PublicTransport_Users1");

                entity.HasOne(d => d.DriverNavigation)
                    .WithMany(p => p.PublicTransportDriverNavigations)
                    .HasForeignKey(d => d.Driver)
                    .HasConstraintName("FK_PublicTransport_Users");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicTransport_Jobs");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicTransport_TaskStatus");

                entity.HasOne(d => d.TransportTypeNavigation)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.TransportType)
                    .HasConstraintName("FK_PublicTransport_PublicTransportTypes");

                entity.HasOne(d => d.CreatedBy)
                   .WithMany(p => p.PublicTransportCreatedBies)
                   .HasForeignKey(d => d.CreatedById);

                entity.Property(e => e.OrganizeNow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.PublicTransportUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<PublicTransportType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(20);
            });


            modelBuilder.HasSequence<int>("JobSeq").StartsAt(2475);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
