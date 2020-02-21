using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.DataBaseHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FarmApp.Infrastructure.Data.Contexts
{
    public class FarmAppContext : DbContext
    {
        public virtual DbSet<ApiMethod> ApiMethods { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<CodeAthType> CodeAthTypes { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionType> RegionTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        public FarmAppContext(DbContextOptions<FarmAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable(Table.Logs, Schema.Log);
                entity.Property(p => p.Method).HasMaxLength(255);
                entity.Property(p => p.Url).HasMaxLength(255);
                entity.Property(p => p.Param).HasMaxLength(8000);
                entity.Property(p => p.Result).HasMaxLength(8000);
                entity.Property(p => p.Exception).HasMaxLength(8000);
            });

            modelBuilder.Entity<ApiMethod>(entity =>
            {
                entity.ToTable(Table.ApiMethod, Schema.Api);
                entity.HasKey(h => h.ApiMethodName).IsClustered();
                entity.Property(p => p.ApiMethodName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.StoredProcedureName).IsRequired().HasMaxLength(350);
                //entity.Property(p => p.IsDisabled).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasData(InitData.InitApiMethods);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(Table.Role, Schema.Dist);
                entity.Property(p => p.RoleName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.IsDisabled).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasData(InitData.InitRoles);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(Table.User, Schema.Dist);
                entity.Property(p => p.Login).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Password).IsRequired().HasMaxLength(20);
                entity.Property(p => p.UserName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsDisabled).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.Role).WithMany(w => w.Users).OnDelete(DeleteBehavior.Restrict);
                entity.HasData(InitData.InitUsers);
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable(Table.Drug, Schema.Tab);
                entity.Property(p => p.DrugName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsGeneric).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.CodeAthType).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Vendor).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CodeAthType>(entity =>
            {
                entity.ToTable(Table.CodeAthType, Schema.Dist);
                entity.Property(p => p.Code).IsRequired().HasMaxLength(50);
                entity.Property(p => p.NameAth).IsRequired().HasMaxLength(350);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.CodeAth).WithMany(w => w.CodeAthTypes).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable(Table.Sale, Schema.Tab);
                entity.Property(p => p.SaleDate).IsRequired().HasColumnType(CommandSql.DateTime);
                entity.Property(p => p.Price).IsRequired().HasColumnType(CommandSql.Money);
                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.IsDiscount).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.Drug).WithMany(w => w.Sales).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Pharmacy).WithMany(w => w.Sales).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.ToTable(Table.Pharmacy, Schema.Dist); 
                entity.Property(p => p.IsMode).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsType).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsNetwork).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDisabled).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.ParentPharmacy).WithMany(w => w.Pharmacies).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Region).WithMany(w => w.Pharmacies).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RegionType>(entity =>
            {
                entity.ToTable(Table.RegionType, Schema.Dist);
                entity.Property(p => p.RegionTypeName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasData(InitData.InitRegionTypes); 
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable(Table.Region, Schema.Dist);
                entity.Property(p => p.RegionName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Population).IsRequired().HasDefaultValueSql(CommandSql.Zero);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.ParentRegion).WithMany(w => w.Regions).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.RegionType).WithMany(w => w.Regions).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable(Table.Vendor, Schema.Dist);
                entity.Property(p => p.VendorName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsDomestic).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
            }); 
        }
    }
}
