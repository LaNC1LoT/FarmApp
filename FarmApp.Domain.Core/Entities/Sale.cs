﻿using FarmApp.Domain.Core.Interfaces;
using System;

namespace FarmApp.Domain.Core.Entities
{
    public class Sale : IEntity
    {
        public long Id { get; set; }
        public int DrugId { get; set; }
        public int PharmacyId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool? IsDiscount { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual Drug Drug { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
    }
}
