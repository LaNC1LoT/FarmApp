﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Region
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public long Population { get; set; }
        public bool IsDeleted { get; set; } 
        //public virtual Region Region { get; set; }

    }
}
