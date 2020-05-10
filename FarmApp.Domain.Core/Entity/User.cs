using FarmApp.Domain.Core.Interfaces;

namespace FarmApp.Domain.Core.Entity
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual RoleType RoleType { get; set; }
    }
}
