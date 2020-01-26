using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    /// <summary>
    /// Роли
    /// </summary>
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
