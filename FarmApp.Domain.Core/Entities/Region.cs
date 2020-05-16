using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entities
{
    public class Region : IEntity
    {
        public Region()
        {
            Regions = new HashSet<Region>();
            Pharmacies = new HashSet<Pharmacy>();
        }
        public int Id { get; set; }
        public int? RegionId { get; set; }
        public int RegionTypeId { get; set; }
        public string RegionName { get; set; }
        public int Population { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual Region ParentRegion { get; set; }
        public virtual RegionType RegionType { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Pharmacy> Pharmacies { get; set; }
    }
}
