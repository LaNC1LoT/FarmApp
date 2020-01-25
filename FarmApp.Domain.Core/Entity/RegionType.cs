using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class RegionType
    {
        public int Id { get; set; }
        public string RegionTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Region> Regions { get; set; }
    }
}
