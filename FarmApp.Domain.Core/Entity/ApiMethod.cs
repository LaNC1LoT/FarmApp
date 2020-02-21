using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class ApiMethod
    {
        public string ApiMethodName { get; set; }
        public string StoredProcedureName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
