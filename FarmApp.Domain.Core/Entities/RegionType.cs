using FarmApp.Domain.Core.Implementations;
using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entities
{
    public class RegionType : EnumEmplimiton, IEntity
    {
        public RegionType()
        {
            Regions = new HashSet<Region>();
        }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
