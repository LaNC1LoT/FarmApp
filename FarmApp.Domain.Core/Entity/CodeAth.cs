using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class CodeAthType
    {
        public int Id { get; set; }
        public int? CodeAthId { get; set; }
        public string Code { get; set; }
        public string NameAth { get; set; }
        public bool IsDeleted { get; set; }
        public CodeAthType ParentCodeAth { get; set; }
    }
}
