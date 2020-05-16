using FarmApp.Domain.Core.Interfaces;

namespace FarmApp.Domain.Core.Entities
{
    public class ApiMethodRole : IEntity
    {
        public int Id { get; set; }
        public int ApiMethodId { get; set; }
        public int RoleTypeId { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ApiMethod ApiMethod { get; set; }
        public virtual RoleType RoleType { get; set; }
    }
}
