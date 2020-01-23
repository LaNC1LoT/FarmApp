using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Drug
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int? VendorId { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsGeneric { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Vendor Vendor { get; set; }
        public User CreatedUser { get; set; }
        public User UpdatedUser { get; set; }
    }
}
