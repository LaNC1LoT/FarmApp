namespace FarmApp.Infrastructure.Business.BusinessModels.Models
{
    public class UserModel : IModel
    {
        public UserFilter UserFilter { get; set; }
        public UserData UserData { get; set; }
    }

    public class UserFilter
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
    public class UserData
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int RoleTypeId { get; set; }
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
