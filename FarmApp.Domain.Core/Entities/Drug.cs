using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entities
{
    public class Drug : IEntity
    {
        public Drug()
        {
            Sales = new HashSet<Sale>();
        }
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int CodeAthTypeId { get; set; }
        public int VendorId { get; set; }
        public bool? IsGeneric { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual CodeAth CodeAth { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
