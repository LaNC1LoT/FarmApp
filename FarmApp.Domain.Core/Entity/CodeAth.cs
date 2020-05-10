using FarmApp.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class CodeAth : IEntity
    {
        public CodeAth()
        {
            ChieldCodeAths = new HashSet<CodeAth>();
            Drugs = new HashSet<Drug>();
        }

        public int Id { get; set; }
        public int? CodeAthId { get; set; }
        public string Code { get; set; }
        public string NameAth { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual CodeAth CodeAths { get; set; }
        public virtual ICollection<CodeAth> ChieldCodeAths { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
