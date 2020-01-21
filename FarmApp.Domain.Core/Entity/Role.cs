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
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public List<User> Users { get; set; }
    }
}
