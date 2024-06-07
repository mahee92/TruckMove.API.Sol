//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace TruckMove.API.DAL.dbFirstModel
//{
//    public partial class TrukMove6Context : DbContext
//    {
//        public TrukMove6Context()
//        {
//        }

//        public TrukMove6Context(DbContextOptions<TrukMove6Context> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<Company> Companies { get; set; } = null!;
//        public virtual DbSet<Contact> Contacts { get; set; } = null!;
//        public virtual DbSet<Job> Jobs { get; set; } = null!;
//        public virtual DbSet<JobSequence> JobSequences { get; set; } = null!;
//        public virtual DbSet<Role> Roles { get; set; } = null!;
//        public virtual DbSet<User> Users { get; set; } = null!;
//        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\localdbtest;Database=TrukMove-6;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Company>(entity =>
//            {
//                entity.HasIndex(e => e.CreatedById, "IX_Companies_CreatedById");

//                entity.HasIndex(e => e.UpdatedById, "IX_Companies_UpdatedById");

//                entity.Property(e => e.AccountsEmail).HasMaxLength(100);

//                entity.Property(e => e.CompanyAbn)
//                    .HasMaxLength(11)
//                    .HasColumnName("CompanyABN");

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

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.IsActive)
//                    .IsRequired()
//                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

//                entity.HasOne(d => d.Company)
//                    .WithMany(p => p.Jobs)
//                    .HasForeignKey(d => d.CompanyId);

//                entity.HasOne(d => d.ControllerNavigation)
//                    .WithMany(p => p.JobControllerNavigations)
//                    .HasForeignKey(d => d.Controller);

//                entity.HasOne(d => d.CreatedBy)
//                    .WithMany(p => p.JobCreatedBies)
//                    .HasForeignKey(d => d.CreatedById);

//                entity.HasOne(d => d.UpdatedBy)
//                    .WithMany(p => p.JobUpdatedBies)
//                    .HasForeignKey(d => d.UpdatedById);
//            });

//            modelBuilder.Entity<JobSequence>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToTable("JobSequence");
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

//            modelBuilder.HasSequence<int>("JobSeq").StartsAt(2475);

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
