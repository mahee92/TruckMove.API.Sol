//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace TruckMove.API.DAL.dbFirst
//{
//    public partial class TrukMovedbFirstContext : DbContext
//    {
//        public TrukMovedbFirstContext()
//        {
//        }

//        public TrukMovedbFirstContext(DbContextOptions<TrukMovedbFirstContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<Company> Companies { get; set; } = null!;
//        public virtual DbSet<Contact> Contacts { get; set; } = null!;
//        public virtual DbSet<Job> Jobs { get; set; } = null!;
//        public virtual DbSet<JobContact> JobContacts { get; set; } = null!;
//        public virtual DbSet<JobSequence> JobSequences { get; set; } = null!;
//        public virtual DbSet<JobStatus> JobStatuses { get; set; } = null!;
//        public virtual DbSet<Note> Notes { get; set; } = null!;
//        public virtual DbSet<PreDepartureChecklist> PreDepartureChecklists { get; set; } = null!;
//        public virtual DbSet<Role> Roles { get; set; } = null!;
//        public virtual DbSet<User> Users { get; set; } = null!;
//        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
//        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
//        public virtual DbSet<VehicleImage> VehicleImages { get; set; } = null!;
//        public virtual DbSet<VehicleNote> VehicleNotes { get; set; } = null!;
//        public virtual DbSet<WayPoint> WayPoints { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\localdbtest;Database=TrukMove-dbFirst;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Company>(entity =>
//            {
//                entity.HasIndex(e => e.CreatedById, "IX_Companies_CreatedById");

//                entity.HasIndex(e => e.UpdatedById, "IX_Companies_UpdatedById");

//                entity.Property(e => e.AccountsEmail).HasMaxLength(100);

//                entity.Property(e => e.CompanyAbn).HasColumnName("CompanyABN");

//                entity.Property(e => e.CompanyName).HasMaxLength(100);

//                entity.Property(e => e.IsActive)
//                    .IsRequired()
//                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

//                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

//                entity.Property(e => e.PrimaryEmail).HasMaxLength(100);

//                entity.HasOne(d => d.CreatedBy)
//                    .WithMany(p => p.CompanyCreatedBies)
//                    .HasForeignKey(d => d.CreatedById);

//                entity.HasOne(d => d.UpdatedBy)
//                    .WithMany(p => p.CompanyUpdatedBies)
//                    .HasForeignKey(d => d.UpdatedById);
//            });

//            modelBuilder.Entity<Contact>(entity =>
//            {
//                entity.HasIndex(e => e.CompanyId, "IX_Contacts_CompanyId");

//                entity.HasIndex(e => e.CreatedById, "IX_Contacts_CreatedById");

//                entity.HasIndex(e => e.UpdatedById, "IX_Contacts_UpdatedById");

//                entity.Property(e => e.IsActive)
//                    .IsRequired()
//                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

//                entity.HasOne(d => d.Company)
//                    .WithMany(p => p.Contacts)
//                    .HasForeignKey(d => d.CompanyId);

//                entity.HasOne(d => d.CreatedBy)
//                    .WithMany(p => p.ContactCreatedBies)
//                    .HasForeignKey(d => d.CreatedById);

//                entity.HasOne(d => d.UpdatedBy)
//                    .WithMany(p => p.ContactUpdatedBies)
//                    .HasForeignKey(d => d.UpdatedById);
//            });

//            modelBuilder.Entity<Job>(entity =>
//            {
//                entity.HasIndex(e => e.CompanyId, "IX_Jobs_CompanyId");

//                entity.HasIndex(e => e.Controller, "IX_Jobs_Controller");

//                entity.HasIndex(e => e.CreatedById, "IX_Jobs_CreatedById");

//                entity.HasIndex(e => e.UpdatedById, "IX_Jobs_UpdatedById");

//                entity.HasIndex(e => e.VehicleId, "UQ_Jobs_VehicleId")
//                    .IsUnique();

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("datetime");

//                entity.Property(e => e.IsActive)
//                    .IsRequired()
//                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

//                entity.Property(e => e.PickupDate).HasColumnType("datetime");

//                entity.HasOne(d => d.Company)
//                    .WithMany(p => p.Jobs)
//                    .HasForeignKey(d => d.CompanyId);

//                entity.HasOne(d => d.ControllerNavigation)
//                    .WithMany(p => p.JobControllerNavigations)
//                    .HasForeignKey(d => d.Controller);

//                entity.HasOne(d => d.CreatedBy)
//                    .WithMany(p => p.JobCreatedBies)
//                    .HasForeignKey(d => d.CreatedById);

//                entity.HasOne(d => d.DriverNavigation)
//                    .WithMany(p => p.JobDriverNavigations)
//                    .HasForeignKey(d => d.Driver)
//                    .HasConstraintName("FK_Jobs_Users");

//                entity.HasOne(d => d.StatusNavigation)
//                    .WithMany(p => p.Jobs)
//                    .HasForeignKey(d => d.Status)
//                    .HasConstraintName("FK_Jobs_JobStatus");

//                entity.HasOne(d => d.UpdatedBy)
//                    .WithMany(p => p.JobUpdatedBies)
//                    .HasForeignKey(d => d.UpdatedById);

//                entity.HasOne(d => d.Vehicle)
//                    .WithOne(p => p.Job)
//                    .HasForeignKey<Job>(d => d.VehicleId)
//                    .HasConstraintName("FK_Jobs_Vehicles");
//            });

//            modelBuilder.Entity<JobContact>(entity =>
//            {
//                entity.HasIndex(e => e.ContactId, "IX_JobContacts_ContactId");

//                entity.HasIndex(e => e.JobId, "IX_JobContacts_JobId");

//                entity.HasOne(d => d.Contact)
//                    .WithMany(p => p.JobContacts)
//                    .HasForeignKey(d => d.ContactId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_JobContacts_Contacts");

//                entity.HasOne(d => d.Job)
//                    .WithMany(p => p.JobContacts)
//                    .HasForeignKey(d => d.JobId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_JobContacts_Jobs");
//            });

//            modelBuilder.Entity<JobSequence>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToTable("JobSequence");
//            });

//            modelBuilder.Entity<JobStatus>(entity =>
//            {
//                entity.ToTable("JobStatus");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Description).HasMaxLength(200);

//                entity.Property(e => e.Status).HasMaxLength(50);
//            });

//            modelBuilder.Entity<Note>(entity =>
//            {
//                entity.Property(e => e.Note1).HasColumnName("Note");

//                entity.HasOne(d => d.Job)
//                    .WithMany(p => p.Notes)
//                    .HasForeignKey(d => d.JobId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_Notes_Jobs");

//                entity.HasOne(d => d.PreDeparturechecklist)
//                    .WithMany(p => p.Notes)
//                    .HasForeignKey(d => d.PreDeparturechecklistId)
//                    .HasConstraintName("FK_Notes_PreDepartureChecklist");

//                entity.HasOne(d => d.Vehicle)
//                    .WithMany(p => p.Notes)
//                    .HasForeignKey(d => d.VehicleId)
//                    .HasConstraintName("FK_Notes_Vehicles");
//            });

//            modelBuilder.Entity<PreDepartureChecklist>(entity =>
//            {
//                entity.ToTable("PreDepartureChecklist");

//                entity.HasIndex(e => e.JobId, "UQ_PreDepartureChecklist_JobId")
//                    .IsUnique();

//                entity.Property(e => e.AirAndElectrics).HasMaxLength(10);

//                entity.Property(e => e.AllLightsAndIndicators).HasMaxLength(10);

//                entity.Property(e => e.CheckInsideTruckTrailer).HasMaxLength(10);

//                entity.Property(e => e.CheckTruckHeight).HasMaxLength(10);

//                entity.Property(e => e.FrontDamage).HasMaxLength(10);

//                entity.Property(e => e.FuelLevel).HasColumnType("decimal(5, 2)");

//                entity.Property(e => e.JackAndTools).HasMaxLength(10);

//                entity.Property(e => e.KeysFobTotalKeys).HasMaxLength(10);

//                entity.Property(e => e.LeftHandDamage).HasMaxLength(10);

//                entity.Property(e => e.Oil).HasMaxLength(10);

//                entity.Property(e => e.OwnersManual).HasMaxLength(10);

//                entity.Property(e => e.RearDamage).HasMaxLength(10);

//                entity.Property(e => e.RightHandDamage).HasMaxLength(10);

//                entity.Property(e => e.SpareRim).HasMaxLength(10);

//                entity.Property(e => e.TyresCondition).HasMaxLength(10);

//                entity.Property(e => e.VehicleCleanFreeOfRubbish).HasMaxLength(10);

//                entity.Property(e => e.VisuallyDipAndCheckTaps).HasMaxLength(10);

//                entity.Property(e => e.Water).HasMaxLength(10);

//                entity.Property(e => e.WindscreenDamageWipers).HasMaxLength(10);

//                entity.HasOne(d => d.Job)
//                    .WithOne(p => p.PreDepartureChecklist)
//                    .HasForeignKey<PreDepartureChecklist>(d => d.JobId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_PreDepartureChecklist_Jobs");
//            });

//            modelBuilder.Entity<Role>(entity =>
//            {
//                entity.Property(e => e.RoleName).HasMaxLength(50);
//            });

//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.HasIndex(e => e.CreatedById, "IX_Users_CreatedById");

//                entity.HasIndex(e => e.UpdatedById, "IX_Users_UpdatedById");

//                entity.Property(e => e.Email).HasMaxLength(100);

//                entity.Property(e => e.FirstName).HasMaxLength(100);

//                entity.Property(e => e.IsActive)
//                    .IsRequired()
//                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

//                entity.Property(e => e.LastName).HasMaxLength(100);

//                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

//                entity.HasOne(d => d.CreatedBy)
//                    .WithMany(p => p.InverseCreatedBy)
//                    .HasForeignKey(d => d.CreatedById);

//                entity.HasOne(d => d.UpdatedBy)
//                    .WithMany(p => p.InverseUpdatedBy)
//                    .HasForeignKey(d => d.UpdatedById);
//            });

//            modelBuilder.Entity<UserRole>(entity =>
//            {
//                entity.ToTable("UserRole");

//                entity.HasIndex(e => e.CreatedById, "IX_UserRole_CreatedById");

//                entity.HasIndex(e => e.RoleId, "IX_UserRole_RoleId");

//                entity.HasIndex(e => e.UpdatedById, "IX_UserRole_UpdatedById");

//                entity.HasIndex(e => e.UserId, "IX_UserRole_UserId");

//                entity.Property(e => e.IsActive)
//                    .IsRequired()
//                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

//                entity.HasOne(d => d.CreatedBy)
//                    .WithMany(p => p.UserRoleCreatedBies)
//                    .HasForeignKey(d => d.CreatedById);

//                entity.HasOne(d => d.Role)
//                    .WithMany(p => p.UserRoles)
//                    .HasForeignKey(d => d.RoleId);

//                entity.HasOne(d => d.UpdatedBy)
//                    .WithMany(p => p.UserRoleUpdatedBies)
//                    .HasForeignKey(d => d.UpdatedById);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.UserRoleUsers)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull);
//            });

//            modelBuilder.Entity<Vehicle>(entity =>
//            {
//                entity.HasIndex(e => e.JobId, "UQ_Vehicles_JobId2")
//                    .IsUnique();

//                entity.Property(e => e.Colour)
//                    .HasMaxLength(100)
//                    .IsFixedLength();

//                entity.Property(e => e.Make)
//                    .HasMaxLength(100)
//                    .IsFixedLength();

//                entity.Property(e => e.Model)
//                    .HasMaxLength(100)
//                    .IsFixedLength();

//                entity.Property(e => e.Rego)
//                    .HasMaxLength(100)
//                    .IsFixedLength();

//                entity.Property(e => e.Vin)
//                    .HasMaxLength(100)
//                    .HasColumnName("VIN")
//                    .IsFixedLength();

//                entity.Property(e => e.Year)
//                    .HasMaxLength(100)
//                    .IsFixedLength();

//                entity.HasOne(d => d.JobNavigation)
//                    .WithOne(p => p.VehicleNavigation)
//                    .HasForeignKey<Vehicle>(d => d.JobId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_Vehicles_Jobs_JobId2");
//            });

//            modelBuilder.Entity<VehicleImage>(entity =>
//            {
//                entity.HasOne(d => d.Vehicle)
//                    .WithMany(p => p.VehicleImages)
//                    .HasForeignKey(d => d.VehicleId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_VehicleImages_Vehicles");
//            });

//            modelBuilder.Entity<VehicleNote>(entity =>
//            {
//                entity.Property(e => e.IsVisibleToDriver)
//                    .IsRequired()
//                    .HasDefaultValueSql("((1))");

//                entity.HasOne(d => d.Vehicle)
//                    .WithMany(p => p.VehicleNotes)
//                    .HasForeignKey(d => d.VehicleId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_VehicleNotes_Vehicles");
//            });

//            modelBuilder.Entity<WayPoint>(entity =>
//            {
//                entity.HasOne(d => d.Job)
//                    .WithMany(p => p.WayPoints)
//                    .HasForeignKey(d => d.JobId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_WayPoints_Jobs");
//            });

//            modelBuilder.HasSequence<int>("JobSeq").StartsAt(2475);

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
