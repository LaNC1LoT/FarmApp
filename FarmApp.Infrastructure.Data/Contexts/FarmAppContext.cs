using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.DataBaseHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FarmApp.Infrastructure.Data.Contexts
{
    public class FarmAppContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<CodeAthType> CodeAthTypes { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionType> RegionTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        public FarmAppContext(DbContextOptions<FarmAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(Table.Role, Schema.Dist);
                entity.Property(p => p.RoleName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.IsDisabled).IsRequired().HasDefaultValue(false);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasData(InitData.InitRoles);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(Table.User, Schema.Dist);
                entity.Property(p => p.UserName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Login).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Password).IsRequired().HasMaxLength(20);
                entity.Property(p => p.IsDisabled).IsRequired().HasDefaultValue(false);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasData(InitData.InitUsers);
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable(Table.Drug, Schema.Tab);
                entity.Property(p => p.DrugName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.IsGeneric).IsRequired().HasDefaultValue(false);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.Property(p => p.CreatedUserId).IsRequired().HasDefaultValue(1);
                entity.Property(p => p.CreatedDate).IsRequired().HasDefaultValueSql(CommandSql.GetDate);
            });

            modelBuilder.Entity<CodeAthType>(entity =>
            {
                entity.ToTable(Table.CodeAthType, Schema.Dist);
                entity.Property(p => p.Code).IsRequired().HasMaxLength(50);
                entity.Property(p => p.NameAth).IsRequired().HasMaxLength(350);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(h => h.CodeAth).WithMany(w => w.CodeAthTypes).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
