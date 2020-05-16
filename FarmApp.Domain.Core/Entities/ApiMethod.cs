using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entities
{
    public class ApiMethod : IEntity
    {
        public ApiMethod()
        {
            ApiMethodRoles = new HashSet<ApiMethodRole>();
        }
        public int Id { get; set; }
        public string ApiMethodName { get; set; }
        public string StoredProcedureName { get; set; }
        public string PathUrl { get; set; }
        public string HttpMethod { get; set; }
        public bool? IsNotNullParam { get; set; }
        public bool? IsNeedAuthentication { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<ApiMethodRole> ApiMethodRoles { get; set; }
    }
}
