using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TruckMove.API.DAL.Models
{
    public partial class TrukMoveLocalContext : DbContext
    {
        public TrukMoveLocalContext(DbContextOptions<TrukMoveLocalContext> options)
            : base(options)
        {
        }

        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\localdbtest;Database=TrukMoveLocal;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CompanyModel>()
              .Property(x => x.Id)
              .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<CompanyModel>()
               .Property(b => b.IsActive)
               .HasDefaultValue(true);

            modelBuilder.Entity<CompanyModel>()
            .HasMany(e => e.Contacts)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired();

            modelBuilder.Entity<ContactModel>()
              .Property(x => x.Id)
              .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<ContactModel>()
               .Property(b => b.IsActive)
            .HasDefaultValue(true);

            modelBuilder.Entity<UserModel>()
              .Property(x => x.Id)
              .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<RoleModel>()
              .Property(x => x.Id)
              .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<RoleModel>().HasData(
            new RoleModel { RoleName = "Administrator", Id = 1 },
             new RoleModel { RoleName = "OpsManager", Id = 2 },
             new RoleModel { RoleName = "AdminTeam", Id = 3 },
              new RoleModel { RoleName = "PayrollTeam", Id = 4 },
              new RoleModel { RoleName = "Drivers", Id = 5 }
        );

            modelBuilder.Entity<CompanyModel>()
           .HasOne(c => c.CreatedBy)
           .WithMany(u => u.CreatedCompanies)
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyModel>()
                .HasOne(c => c.UpdatedBy)
                .WithMany(u => u.UpdatedCompanies)
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ContactModel>()
           .HasOne(c => c.CreatedBy)
           .WithMany(u => u.CreatedContacts)
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContactModel>()
                .HasOne(c => c.UpdatedBy)
                .WithMany(u => u.UpdatedContacts)
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRoleModel>()
           .HasOne(c => c.CreatedBy)
           .WithMany(u => u.CreatedRoles)
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(c => c.UpdatedBy)
                .WithMany(u => u.UpdatedRoles)
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
             .Property(b => b.IsActive)
             .HasDefaultValue(true);

            modelBuilder.Entity<UserRoleModel>()
             .Property(b => b.IsActive)
             .HasDefaultValue(true);

            modelBuilder.Entity<UserModel>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
