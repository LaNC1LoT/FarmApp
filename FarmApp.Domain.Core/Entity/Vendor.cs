using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class Vendor : IEntity
    {
        public Vendor()
        {
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public string VendorName { get; set; }
        public bool? IsDomestic { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
