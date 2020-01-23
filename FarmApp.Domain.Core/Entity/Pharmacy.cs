using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            Pharmacies = new HashSet<Pharmacy>();
        }

        public int Id { get; set; }
        public int? PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int RegionId { get; set; }
        public bool? IsMode { get; set; }
        public bool? IsType { get; set; }
        public bool? IsNetwork { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Pharmacy ParentPharmacy { get; set; }
        public virtual Region Region { get; set; }
        public virtual User CreatedUser { get; set; }
        public virtual User UpdatedUser { get; set; }
        public virtual ICollection<Pharmacy> Pharmacies { get; set; }
    }
}
