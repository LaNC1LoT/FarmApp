using FarmApp.Domain.Core.Implementations;
using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entities
{
    /// <summary>
    /// Справочник ролей
    /// </summary>
    public class RoleType : EnumEmplimiton, IEntity
    {
        public RoleType()
        {
            Users = new HashSet<User>();
            ApiMethodRoles = new HashSet<ApiMethodRole>();
        }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ApiMethodRole> ApiMethodRoles { get; set; }
    }
}
