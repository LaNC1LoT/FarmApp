using FarmApp.Domain.Core.Entity;
using FarmApp.Domain.Core.Enums;
using System;
using System.Collections.Generic;

namespace FarmApp.Infrastructure.Data.DataBaseHelper
{
    internal class InitData : IDisposable
    {
        public IEnumerable<RoleType> InitRoles()
        {
            return typeof(Roles).EnumToList<RoleType>();
        }

        public IEnumerable<RegionType> InitRegionTypes()
        {
            return typeof(RegionTypes).EnumToList<RegionType>();
        }

        public IEnumerable<User> InitUsers { get; private set; } = new List<User>(2)
        {
            new User { Id = 1, UserName = "Админ", Login = "admin", Password = "123456", RoleId = (int)Roles.Admin },
            new User { Id = 2, UserName = "Пользователь", Login = "user", Password = "123456", RoleId = (int)Roles.User },
        };



        public IEnumerable<ApiMethod> InitApiMethods { get; private set; } = new List<ApiMethod>(2)
        {
            new ApiMethod{ Id = 1, ApiMethodName = "GetToken", StoredProcedureName = null, PathUrl = "/GetToken", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = false, IsDeleted = false },
            new ApiMethod{ Id = 2, ApiMethodName = "GetUser", StoredProcedureName = null, PathUrl = "/GetUser", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false }
        };

        public IEnumerable<ApiMethodRole> InitApitMethodRoles { get; private set; } = new List<ApiMethodRole>(3)
        {
            new ApiMethodRole{ Id = 1, ApiMethodId = 1, RoleId = 1, IsDeleted = false },
            new ApiMethodRole{ Id = 2, ApiMethodId = 1, RoleId = 2, IsDeleted = false },
            new ApiMethodRole{ Id = 3, ApiMethodId = 2, RoleId = 1, IsDeleted = false }
        };

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

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                InitUsers = null;
                InitApiMethods = null;
                InitApitMethodRoles = null;

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~InitData()
        // {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        void IDisposable.Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
