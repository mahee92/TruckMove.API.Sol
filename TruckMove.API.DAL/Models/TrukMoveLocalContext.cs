using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

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
               .Property(b => b.IsActive)
               .HasDefaultValue(true);

            modelBuilder.Entity<ContactModel>()
               .Property(b => b.IsActive)
            .HasDefaultValue(true);


            modelBuilder.Entity<CompanyModel>()
             .HasMany(e => e.Contacts)
             .WithOne(e => e.Company)
             .HasForeignKey(e => e.CompanyId)
             .IsRequired();




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
