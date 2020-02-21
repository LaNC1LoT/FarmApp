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

        public static IEnumerable<RegionType> InitRegionTypes
        {
            get => new List<RegionType>(5)
            {
                new RegionType{ Id = 1, RegionTypeName = "Государство" },
                new RegionType{ Id = 2, RegionTypeName = "Субъект(регион)" },
                new RegionType{ Id = 3, RegionTypeName = "Город" },
                new RegionType{ Id = 4, RegionTypeName = "Сёла, деревни и др." },
                new RegionType{ Id = 5, RegionTypeName = "Микрорайон" }
            };
        }

        public static IEnumerable<ApiMethod> InitApiMethods
        {
            get => new List<ApiMethod>(2)
            {
                new ApiMethod{ ApiMethodName = "LoginUser", StoredProcedureName = "UserAutification", IsDeleted = false },
                new ApiMethod{ ApiMethodName = "GetUsers", StoredProcedureName = "GetUsers", IsDeleted = false },
                new ApiMethod{ ApiMethodName = "UpSertUser", StoredProcedureName = "UpSertUser", IsDeleted = false }
            };
        }

        //public static IEnumerable<Region> InitRegions
        //{
        //    get => new List<Region>()
        //    {
        //        new Region{ Id = 1, RegionId = null, RegionTypeId = 1, RegionName = "Приднестровье", Population = 469000 },
        //        new Region{ Id = 2, RegionId = 1, RegionTypeId = 2, RegionName = "Григориопольский район", Population = 38694 },
        //        new Region{ Id = 3, RegionId = 1, RegionTypeId = 2, RegionName = "Дубоссарский район", Population = 30491 },
        //        new Region{ Id = 4, RegionId = 1, RegionTypeId = 2, RegionName = "Каменский район", Population = 19681 },
        //        new Region{ Id = 5, RegionId = 1, RegionTypeId = 2, RegionName = "Рыбницкий район", Population = 67659 },
        //        new Region{ Id = 6, RegionId = 1, RegionTypeId = 2, RegionName = "Слободзейский район", Population = 82303 },
        //        new Region{ Id = 7, RegionId = 1, RegionTypeId = 2, RegionName = "Тирасполь", Population = 133807 },
        //        new Region{ Id = 8, RegionId = 1, RegionTypeId = 2, RegionName = "Бендеры", Population = 91882 },
        //        new Region{ Id = 9, RegionId = 7, RegionTypeId = 3, RegionName = "Тирасполь", Population = 133807 },
        //        new Region{ Id = 10, RegionId = 8, RegionTypeId = 3, RegionName = "Бендеры", Population = 91882 },

        //    };
        //}
    }
}
