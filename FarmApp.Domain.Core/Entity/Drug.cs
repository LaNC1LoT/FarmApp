using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Drug
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public bool IsGeneric { get; set; }
        public bool IsDeleted { get; set; }
    }
}
