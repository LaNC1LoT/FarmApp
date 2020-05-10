using System;

namespace FarmApp.Domain.Core.Attributes
{
    public class EnumHelperAttribute : Attribute
    {
        public string Description { get; private set; }
        public bool IsDeleted { get; private set; }

        public EnumHelperAttribute()
        {
            Description = "Не указано";
            IsDeleted = false;
        }

        public EnumHelperAttribute(string description, bool isDeleted)
        {
            Description = description;
            IsDeleted = isDeleted;
        }
    }
}
