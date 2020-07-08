using System.Collections.Generic;

namespace FarmAppServer.BusinessModels
{
    public class UserView
    {
        public IEnumerable<UserFilter> UserFilter { get; set; }
        public IEnumerable<UserData> UserData { get; set; }
    }

    public class UserAutorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UserToken
    {
        public IEnumerable<ModelState> Validations { get; set; }
        public UserLogin UserLogin { get; set; }
    }

    public class UserCrud
    {
        public IEnumerable<ModelState> Validations { get; set; }
        public UserData User { get; set; }
    }

    public class UserLogin
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
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
