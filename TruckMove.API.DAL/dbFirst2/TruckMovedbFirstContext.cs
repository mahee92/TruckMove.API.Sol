using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TruckMove.API.DAL.dbFirst2
{
    public partial class TruckMovedbFirstContext : DbContext
    {
        public TruckMovedbFirstContext()
        {
        }

        public TruckMovedbFirstContext(DbContextOptions<TruckMovedbFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accommodation> Accommodations { get; set; } = null!;
        public virtual DbSet<Acknowledgement> Acknowledgements { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<CheckListImage> CheckListImages { get; set; } = null!;
        public virtual DbSet<Checklist> Checklists { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Delay> Delays { get; set; } = null!;
        public virtual DbSet<DelayDriver> DelayDrivers { get; set; } = null!;
        public virtual DbSet<HookupType> HookupTypes { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobContact> JobContacts { get; set; } = null!;
        public virtual DbSet<JobSequence> JobSequences { get; set; } = null!;
        public virtual DbSet<JobStatus> JobStatuses { get; set; } = null!;
        public virtual DbSet<Leg> Legs { get; set; } = null!;
        public virtual DbSet<LegStatus> LegStatuses { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public virtual DbSet<PermitsAndPlate> PermitsAndPlates { get; set; } = null!;
        public virtual DbSet<PublicTransport> PublicTransports { get; set; } = null!;
        public virtual DbSet<PublicTransportType> PublicTransportTypes { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Rate> Rates { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; } = null!;
        public virtual DbSet<Trailer> Trailers { get; set; } = null!;
        public virtual DbSet<TrailerStatus> TrailerStatuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Variance> Variances { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<WayPoint> WayPoints { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.111.111.23;Database=TruckMove-dbFirst;User Id=dev1;Password=hfjdhfkjkdsfd787*Fg;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accommodation>(entity =>
            {
                entity.ToTable("Accommodation");

                entity.HasIndex(e => e.Assignee, "IX_Accommodation_Assignee");

                entity.HasIndex(e => e.CreatedById, "IX_Accommodation_CreatedById");

                entity.HasIndex(e => e.Driver, "IX_Accommodation_Driver");

                entity.HasIndex(e => e.JobId, "IX_Accommodation_JobId");

                entity.HasIndex(e => e.Status, "IX_Accommodation_Status");

                entity.HasIndex(e => e.UpdatedById, "IX_Accommodation_UpdatedById");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ReferenceNumber).HasMaxLength(50);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.AccommodationAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_Accommodation_Users1");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.AccommodationCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

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

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.AccommodationUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<Acknowledgement>(entity =>
            {
                entity.ToTable("Acknowledgement");

                entity.HasIndex(e => e.JobId, "IX_Acknowledgement_JobId");

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

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasIndex(e => e.AccommodationId, "IX_Attachments_AccommodationId");

                entity.HasIndex(e => e.CreatedById, "IX_Attachments_CreatedById");

                entity.HasIndex(e => e.PermitAndPlateId, "IX_Attachments_PermitAndPlateId");

                entity.HasIndex(e => e.PublicTransportId, "IX_Attachments_PublicTransportId");

                entity.HasIndex(e => e.UpdatedById, "IX_Attachments_UpdatedById");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Accommodation)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AccommodationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TaskAttachments_Accommodation");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.AttachmentCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.PermitAndPlate)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.PermitAndPlateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TaskAttachments_PermitsAndPlates");

                entity.HasOne(d => d.PublicTransport)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.PublicTransportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Attachments_PublicTransport");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.AttachmentUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<CheckListImage>(entity =>
            {
                entity.HasIndex(e => e.ChecklistId, "IX_CheckListImages_ChecklistId");

                entity.Property(e => e.Url).HasColumnName("url");

                entity.HasOne(d => d.Checklist)
                    .WithMany(p => p.CheckListImages)
                    .HasForeignKey(d => d.ChecklistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckListPhotos_Checklist");
            });

            modelBuilder.Entity<Checklist>(entity =>
            {
                entity.ToTable("Checklist");

                entity.HasIndex(e => e.CreatedById, "IX_Checklist_CreatedById");

                entity.HasIndex(e => e.JobId, "IX_Checklist_JobId");

                entity.HasIndex(e => e.UpdatedById, "IX_Checklist_UpdatedById");

                entity.Property(e => e.AirAndElectrics).HasMaxLength(10);

                entity.Property(e => e.AllLightsAndIndicators).HasMaxLength(10);

                entity.Property(e => e.CheckInsideTruckTrailer).HasMaxLength(10);

                entity.Property(e => e.CheckTruckHeight).HasMaxLength(10);

                entity.Property(e => e.FrontDamage).HasMaxLength(10);

                entity.Property(e => e.FuelLevel).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsPre)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

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

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.ChecklistCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Checklists)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreDepartureChecklist_Jobs");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.ChecklistUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Companies_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Companies_UpdatedById");

                entity.Property(e => e.AccountsEmail).HasMaxLength(100);

                entity.Property(e => e.CompanyAbn).HasColumnName("CompanyABN");

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

            modelBuilder.Entity<Delay>(entity =>
            {
                entity.HasIndex(e => e.Assignee, "IX_Delays_Assignee");

                entity.HasIndex(e => e.CreatedById, "IX_Delays_CreatedById");

                entity.HasIndex(e => e.JobId, "IX_Delays_JobId");

                entity.HasIndex(e => e.Status, "IX_Delays_Status");

                entity.HasIndex(e => e.UpdatedById, "IX_Delays_UpdatedById");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.DelayAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_Delays_Users");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.DelayCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Delays)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delays_Jobs");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Delays)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delays_TaskStatus");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.DelayUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<DelayDriver>(entity =>
            {
                entity.HasIndex(e => e.DelayId, "IX_DelayDrivers_DelayId");

                entity.HasIndex(e => e.DriverId, "IX_DelayDrivers_DriverId");

                entity.HasIndex(e => e.PaymentStatus, "IX_DelayDrivers_PaymentStatus");

                entity.Property(e => e.PaymentStatus).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Delay)
                    .WithMany(p => p.DelayDrivers)
                    .HasForeignKey(d => d.DelayId)
                    .HasConstraintName("FK_DelayDrivers_Delays");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DelayDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelayDrivers_Users");

                entity.HasOne(d => d.PaymentStatusNavigation)
                    .WithMany(p => p.DelayDrivers)
                    .HasForeignKey(d => d.PaymentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelayDrivers_PaymentStatus");
            });

            modelBuilder.Entity<HookupType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Images_CreatedById");

                entity.HasIndex(e => e.JobId, "IX_Images_JobId");

                entity.HasIndex(e => e.TrailerId, "IX_Images_TrailerId");

                entity.HasIndex(e => e.UpdatedById, "IX_Images_UpdatedById");

                entity.HasIndex(e => e.VehicleId, "IX_Images_VehicleId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.ImageCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Images_Jobs");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.TrailerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Images_Trailers");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.ImageUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);

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

                entity.HasIndex(e => e.Driver, "IX_Jobs_Driver");

                entity.HasIndex(e => e.Status, "IX_Jobs_Status");

                entity.HasIndex(e => e.UpdatedById, "IX_Jobs_UpdatedById");

                entity.HasIndex(e => e.VehicleId, "UQ_Jobs_VehicleId")
                    .IsUnique()
                    .HasFilter("([VehicleId] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsCommercialLoad)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsDangerousGoods)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

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

                entity.HasIndex(e => e.CreatedById, "IX_JobContacts_CreatedById");

                entity.HasIndex(e => e.JobId, "IX_JobContacts_JobId");

                entity.HasIndex(e => e.UpdatedById, "IX_JobContacts_UpdatedById");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobContacts_Contacts");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.JobContactCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobContacts_Jobs");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.JobContactUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
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

                entity.Property(e => e.LightColour).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Leg>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Legs_CreatedById");

                entity.HasIndex(e => e.DriverId, "IX_Legs_DriverId");

                entity.HasIndex(e => e.JobId, "IX_Legs_JobId");

                entity.HasIndex(e => e.PaymentStatus, "IX_Legs_PaymentStatus");

                entity.HasIndex(e => e.Status, "IX_Legs_Status");

                entity.HasIndex(e => e.UpdatedById, "IX_Legs_UpdatedById");

                entity.HasIndex(e => e.Variance, "IX_Legs_Variance");

                entity.Property(e => e.EndLocation).HasMaxLength(500);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.PaymentStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartLocation)
                    .HasMaxLength(500)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.000')");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.LegCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.LegDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_Users");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Legs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_Jobs");

                entity.HasOne(d => d.PaymentStatusNavigation)
                    .WithMany(p => p.Legs)
                    .HasForeignKey(d => d.PaymentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_PaymentStatus");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Legs)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Legs_LegStatus");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.LegUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);

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

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasIndex(e => e.AccommodationId, "IX_Notes_AccommodationId");

                entity.HasIndex(e => e.ChecklistId, "IX_Notes_ChecklistId");

                entity.HasIndex(e => e.CreatedById, "IX_Notes_CreatedById");

                entity.HasIndex(e => e.DelayId, "IX_Notes_DelayId");

                entity.HasIndex(e => e.JobId, "IX_Notes_JobId");

                entity.HasIndex(e => e.PermitAndPlatesId, "IX_Notes_PermitAndPlatesId");

                entity.HasIndex(e => e.PublicTransportId, "IX_Notes_PublicTransportId");

                entity.HasIndex(e => e.TrailerId, "IX_Notes_TrailerId");

                entity.HasIndex(e => e.UpdatedById, "IX_Notes_UpdatedById");

                entity.HasIndex(e => e.VehicleId, "IX_Notes_VehicleId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Accommodation)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.AccommodationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notes_Accommodation");

                entity.HasOne(d => d.Checklist)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.ChecklistId)
                    .HasConstraintName("FK_Notes_Checklist");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.NoteCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.Delay)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.DelayId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notes_Delays");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notes_Jobs");

                entity.HasOne(d => d.PermitAndPlates)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PermitAndPlatesId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notes_PermitsAndPlates");

                entity.HasOne(d => d.PublicTransport)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PublicTransportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notes_PublicTransport");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.TrailerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notes_Trailers");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.NoteUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Notes_Vehicles");
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<PermitsAndPlate>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_PermitsAndPlate_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_PermitsAndPlate_UpdatedById");

                entity.HasIndex(e => e.Assignee, "IX_PermitsAndPlates_Assignee");

                entity.HasIndex(e => e.JobId, "IX_PermitsAndPlates_JobId");

                entity.HasIndex(e => e.Status, "IX_PermitsAndPlates_Status");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.OrganizeNow).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.PermitNumber).HasMaxLength(50);

                entity.Property(e => e.PlateNumber).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.PermitsAndPlateAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_PermitsAndPlates_Users");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.PermitsAndPlateCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

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

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.PermitsAndPlateUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<PublicTransport>(entity =>
            {
                entity.ToTable("PublicTransport");

                entity.HasIndex(e => e.Assignee, "IX_PublicTransport_Assignee");

                entity.HasIndex(e => e.CreatedById, "IX_PublicTransport_CreatedById");

                entity.HasIndex(e => e.Driver, "IX_PublicTransport_Driver");

                entity.HasIndex(e => e.JobId, "IX_PublicTransport_JobId");

                entity.HasIndex(e => e.PaymentStatus, "IX_PublicTransport_PaymentStatus");

                entity.HasIndex(e => e.Status, "IX_PublicTransport_Status");

                entity.HasIndex(e => e.TransportType, "IX_PublicTransport_TransportType");

                entity.HasIndex(e => e.UpdatedById, "IX_PublicTransport_UpdatedById");

                entity.Property(e => e.ArrivalDateTime).HasColumnType("datetime");

                entity.Property(e => e.Daterequired).HasColumnType("datetime");

                entity.Property(e => e.DepartureDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PaymentStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReferenceNumber).HasMaxLength(100);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.PublicTransportAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_PublicTransport_Users1");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.PublicTransportCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.DriverNavigation)
                    .WithMany(p => p.PublicTransportDriverNavigations)
                    .HasForeignKey(d => d.Driver)
                    .HasConstraintName("FK_PublicTransport_Users");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicTransport_Jobs");

                entity.HasOne(d => d.PaymentStatusNavigation)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.PaymentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicTransport_PaymentStatus");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicTransport_TaskStatus");

                entity.HasOne(d => d.TransportTypeNavigation)
                    .WithMany(p => p.PublicTransports)
                    .HasForeignKey(d => d.TransportType)
                    .HasConstraintName("FK_PublicTransport_PublicTransportTypes");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.PublicTransportUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<PublicTransportType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(20);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchase");

                entity.HasIndex(e => e.Assignee, "IX_Purchase_Assignee");

                entity.HasIndex(e => e.CreatedById, "IX_Purchase_CreatedById");

                entity.HasIndex(e => e.Driver, "IX_Purchase_Driver");

                entity.HasIndex(e => e.JobId, "IX_Purchase_JobId");

                entity.HasIndex(e => e.PaymentStatus, "IX_Purchase_PaymentStatus");

                entity.HasIndex(e => e.Status, "IX_Purchase_Status");

                entity.HasIndex(e => e.UpdatedById, "IX_Purchase_UpdatedById");

                entity.Property(e => e.FromMobile)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.PaymentStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.Vendor).HasMaxLength(200);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.PurchaseAssigneeNavigations)
                    .HasForeignKey(d => d.Assignee)
                    .HasConstraintName("FK_Purchase_Users1");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.PurchaseCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.DriverNavigation)
                    .WithMany(p => p.PurchaseDriverNavigations)
                    .HasForeignKey(d => d.Driver)
                    .HasConstraintName("FK_Purchase_Users");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Jobs");

                entity.HasOne(d => d.PaymentStatusNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PaymentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_PaymentStatus");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_TaskStatus");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.PurchaseUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Rates_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Rates_UpdatedById");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Value).HasDefaultValueSql("(CONVERT([float],(0)))");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.RateCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.RateUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Trailers_CreatedById");

                entity.HasIndex(e => e.HookupLeg, "IX_Trailers_HookupLeg");

                entity.HasIndex(e => e.HookupType, "IX_Trailers_HookupType");

                entity.HasIndex(e => e.JobId, "IX_Trailers_JobId");

                entity.HasIndex(e => e.Status, "IX_Trailers_Status");

                entity.HasIndex(e => e.UpdatedById, "IX_Trailers_UpdatedById");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.Rego).HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.TrailerCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.HookupLegNavigation)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.HookupLeg)
                    .HasConstraintName("FK_Trailers_Legs");

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

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Trailers_TrailerStatus");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.TrailerUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<TrailerStatus>(entity =>
            {
                entity.ToTable("TrailerStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status).HasMaxLength(50);
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

            modelBuilder.Entity<Variance>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.CreatedById, "IX_Vehicles_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Vehicles_UpdatedById");

                entity.HasIndex(e => e.JobId, "UQ_Vehicles_JobId")
                    .IsUnique();

                entity.Property(e => e.Colour).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Make).HasMaxLength(100);

                entity.Property(e => e.Model).HasMaxLength(100);

                entity.Property(e => e.Rego).HasMaxLength(100);

                entity.Property(e => e.Vin)
                    .HasMaxLength(100)
                    .HasColumnName("VIN");

                entity.Property(e => e.Year).HasMaxLength(100);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.VehicleCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

                entity.HasOne(d => d.JobNavigation)
                    .WithOne(p => p.VehicleNavigation)
                    .HasForeignKey<Vehicle>(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.VehicleUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById);
            });

            modelBuilder.Entity<WayPoint>(entity =>
            {
                entity.HasIndex(e => e.JobId, "IX_WayPoints_JobId");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.WayPoints)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WayPoints_Jobs");
            });

            modelBuilder.HasSequence<int>("JobSeq").StartsAt(2475);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
