﻿using FarmApp.Domain.Core.Entities;
using FarmApp.Infrastructure.Data.DataBaseHelper;
using Microsoft.EntityFrameworkCore;

namespace FarmApp.Infrastructure.Data.Contexts
{
    public class FarmAppContext : DbContext
    {
        public FarmAppContext(DbContextOptions<FarmAppContext> options)
            : base(options)
        { }

        public virtual DbSet<ApiMethod> ApiMethods { get; set; }
        public virtual DbSet<ApiMethodRole> ApiMethodRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<CodeAth> CodeAths { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionType> RegionTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            using (InitData initData = new InitData())
            {
                modelBuilder.Entity<Log>(entity =>
                {
                    entity.ToTable(Table.Logs, Schema.Log);
                    entity.Property(p => p.UserId).HasMaxLength(50);
                    entity.Property(p => p.RoleId).HasMaxLength(50);
                    entity.Property(p => p.MethodRoute).HasMaxLength(255);
                    entity.Property(p => p.HeaderRequest).HasMaxLength(4000);
                    entity.Property(p => p.HttpMethod).HasMaxLength(255);
                    entity.Property(p => p.PathUrl).HasMaxLength(255);
                    entity.Property(p => p.Param).HasMaxLength(4000);
                    entity.Property(p => p.HeaderResponse).HasMaxLength(4000);
                    entity.Property(p => p.Header).HasMaxLength(255);
                    entity.Property(p => p.Result).HasMaxLength(4000);
                    entity.Property(p => p.Exception).HasMaxLength(4000);
                });

                modelBuilder.Entity<RoleType>(entity =>
                {
                    entity.ToTable(Table.RoleType, Schema.Dist);
                    entity.Property(p => p.EnumName).IsRequired().HasMaxLength(50);
                    entity.Property(p => p.Description).IsRequired().HasMaxLength(50);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasData(initData.InitRoles());
                });

                modelBuilder.Entity<ApiMethod>(entity =>
                {
                    entity.ToTable(Table.ApiMethod, Schema.Api);
                    entity.Property(p => p.ApiMethodName).IsRequired().HasMaxLength(350);
                    entity.HasIndex(p => p.ApiMethodName).IsUnique();
                    entity.Property(p => p.StoredProcedureName).HasMaxLength(350);
                    entity.Property(p => p.PathUrl).IsRequired().HasMaxLength(350);
                    entity.Property(p => p.HttpMethod).IsRequired().HasMaxLength(350);
                    entity.Property(p => p.IsNotNullParam).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.Property(p => p.IsNeedAuthentication).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasData(initData.InitApiMethods);
                });

                modelBuilder.Entity<ApiMethodRole>(entity =>
                {
                    entity.ToTable(Table.ApiMethodRole, Schema.Api);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasOne(p => p.RoleType).WithMany(w => w.ApiMethodRoles).OnDelete(DeleteBehavior.Restrict);
                    entity.HasOne(p => p.ApiMethod).WithMany(w => w.ApiMethodRoles).OnDelete(DeleteBehavior.Restrict);
                    entity.HasData(initData.InitApitMethodRoles);
                });

                modelBuilder.Entity<User>(entity =>
                {
                    entity.ToTable(Table.User, Schema.Dist);
                    entity.Property(p => p.Login).IsRequired().HasMaxLength(20);
                    entity.Property(p => p.Password).IsRequired().HasMaxLength(20);
                    entity.Property(p => p.UserName).IsRequired().HasMaxLength(255);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasOne(h => h.RoleType).WithMany(w => w.Users).OnDelete(DeleteBehavior.Restrict);
                    entity.HasData(initData.InitUsers);
                });

                modelBuilder.Entity<Drug>(entity =>
                {
                    entity.ToTable(Table.Drug, Schema.Tab);
                    entity.Property(p => p.DrugName).IsRequired().HasMaxLength(255);
                    entity.Property(p => p.IsGeneric).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasOne(h => h.CodeAth).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
                    entity.HasOne(h => h.Vendor).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CodeAth>(entity =>
                {
                    entity.ToTable(Table.CodeAth, Schema.Dist);
                    entity.Property(p => p.Code).IsRequired().HasMaxLength(50);
                    entity.Property(p => p.NameAth).IsRequired().HasMaxLength(350);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasOne(h => h.CodeAths).WithMany(w => w.ChieldCodeAths).OnDelete(DeleteBehavior.Restrict);
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
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasOne(h => h.ParentPharmacy).WithMany(w => w.Pharmacies).OnDelete(DeleteBehavior.Restrict);
                    entity.HasOne(h => h.Region).WithMany(w => w.Pharmacies).OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<RegionType>(entity =>
                {
                    entity.ToTable(Table.RegionType, Schema.Dist);
                    entity.Property(p => p.EnumName).IsRequired().HasMaxLength(50);
                    entity.Property(p => p.Description).IsRequired().HasMaxLength(50);
                    entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                    entity.HasData(initData.InitRegionTypes());
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
}
