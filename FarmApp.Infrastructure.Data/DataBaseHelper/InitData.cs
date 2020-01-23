using FarmApp.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Infrastructure.Data.DataBaseHelper
{
    public class InitData
    {
        public static IEnumerable<Role> InitRoles
        {
            get => new List<Role>(2)
            {
                new Role { Id = 1, RoleName = "admin" },
                new Role { Id = 2, RoleName = "user" },
            };
        }

        public static IEnumerable<User> InitUsers
        {
            get => new List<User>(2)
            {
                new User { Id = 1, UserName = "Админ", Login = "admin", Password = "123456", RoleId = 1 },
                new User { Id = 2, UserName = "Пользователь", Login = "user", Password = "123456", RoleId = 2 },
            };
        }
    }
}
