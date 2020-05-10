using FarmApp.Domain.Core.Interfaces;

namespace FarmApp.Domain.Core.Implementations
{
    public abstract class EnumEmplimiton : IEnum
    {
        public int Id { get; set; }
        public string EnumName { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
