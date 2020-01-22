﻿using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Schemas;
using FarmApp.Infrastructure.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmApp.Infrastructure.Data.Contexts
{
    public class FarmAppContext : DbContext
    {
        public FarmAppContext(DbContextOptions<FarmAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRole = new Role { Id = 1, RoleName = "admin" };
            var userRole = new Role { Id = 2, RoleName = "user" };
            var userRole2 = new Role { Id = 3, RoleName = "user2" };
            var adminUser1 = new User { Id = 1, UserName = "Админ", Login = "admin", Password = "123456", RoleId = adminRole.Id };
            var simpleUser2 = new User { Id = 2, UserName = "Пользователь", Login = "user", Password = "123456", RoleId = userRole.Id };

            var user = modelBuilder.Entity<User>();
            user.ToTable(Table.User, Schema.Dist);
            user.Property(p => p.UserName).IsRequired().HasMaxLength(255);
            user.Property(p => p.Login).IsRequired().HasMaxLength(20);
            user.Property(p => p.Password).IsRequired().HasMaxLength(20);
            user.Property(p => p.IsDisabled).IsRequired().HasDefaultValue(false);
            user.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

            var role = modelBuilder.Entity<Role>();
            role.ToTable(Table.Role, Schema.Dist);
            role.Property(p => p.RoleName).IsRequired().HasMaxLength(50);
            role.Property(p => p.IsDisabled).IsRequired().HasDefaultValue(false);
            role.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

            var drug = modelBuilder.Entity<Drug>();
            drug.ToTable(Table.Drug, Schema.Tab);
            drug.Property(p => p.DrugName).IsRequired().HasMaxLength(50);
            drug.Property(p => p.IsGeneric).IsRequired().HasDefaultValue(false);
            drug.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
            drug.Property(p => p.CreatedUserId).IsRequired().HasDefaultValueSql("1");
            drug.Property(p => p.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");

            var codeAth = modelBuilder.Entity<CodeAthType>();
            codeAth.ToTable(Table.CodeAthType, Schema.Dist);
            codeAth.Property(p => p.Code).IsRequired().HasMaxLength(50);
            codeAth.Property(p => p.NameAth).IsRequired().HasMaxLength(350);
            codeAth.Property(p => p.NameAth).IsRequired().HasDefaultValue(false);

            foreach (var relShip in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relShip.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Role>().HasData(adminRole, userRole, userRole2);
            modelBuilder.Entity<User>().HasData(adminUser1, simpleUser2);
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<CodeAthType> CodeAthTypes { get; set; }

    }
}
