using FarmApp.Service.Interfaces.BisinessModels.Models;

namespace FarmApp.Service.Interfaces.BisinessModels
{
    public interface IUserDto : IData
    {
        int Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
        int RoleTypeId { get; set; }
        string RoleName { get; set; }
        bool? IsDeleted { get; set; }
    }
}
