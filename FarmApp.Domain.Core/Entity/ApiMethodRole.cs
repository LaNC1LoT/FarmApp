using FarmApp.Domain.Core.Interfaces;

namespace FarmApp.Domain.Core.Entity
{
    public class ApiMethodRole : IEntity
    {
        public int Id { get; set; }
        public int ApiMethodId { get; set; }
        public int RoleId { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ApiMethod ApiMethod { get; set; }
        public virtual RoleType RoleType { get; set; }
    }
}
