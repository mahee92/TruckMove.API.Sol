using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobContact> JobContacts { get; set; } = null!;
        public virtual DbSet<JobSequence> JobSequences { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

        public virtual DbSet<VehicleImage> VehicleImages { get; set; } = null!;
 	   public virtual DbSet<VehicleNote> VehicleNotes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\localdbtest;Database=TrukMove-11;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
          new Role { Id = (int)RoleEnum.Administrator, RoleName = RoleEnum.Administrator.ToString() },
          new Role { Id = (int)RoleEnum.OpsManager, RoleName = RoleEnum.OpsManager.ToString() },
          new Role { Id = (int)RoleEnum.AdminTeam, RoleName = RoleEnum.AdminTeam.ToString() },
          new Role { Id = (int)RoleEnum.PayrollTeam, RoleName = RoleEnum.PayrollTeam.ToString() },
          new Role { Id = (int)RoleEnum.Drivers, RoleName = RoleEnum.Drivers.ToString() }
          );
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

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_Jobs_CompanyId");

                entity.HasIndex(e => e.Controller, "IX_Jobs_Controller");

                entity.HasIndex(e => e.CreatedById, "IX_Jobs_CreatedById");

                entity.HasIndex(e => e.UpdatedById, "IX_Jobs_UpdatedById");

                entity.HasIndex(e => e.VehicleId, "UQ_Jobs_VehicleId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.ControllerNavigation)
                    .WithMany(p => p.JobControllerNavigations)
                    .HasForeignKey(d => d.Controller);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.JobCreatedBies)
                    .HasForeignKey(d => d.CreatedById);

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
            modelBuilder.Entity<VehicleImage>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");
               
                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleImages)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleImages_Vehicles");
            });

            modelBuilder.Entity<VehicleNote>(entity =>
            {
                entity.Property(e => e.IsVisibleToDriver)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleNotes)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleNotes_Vehicles");
            });

            modelBuilder.HasSequence<int>("JobSeq").StartsAt(2475);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
