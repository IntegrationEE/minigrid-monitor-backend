using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Programme : BaseEntity
    {
        public Programme()
        {
            Sites = new HashSet<Site>();
            Indicators = new HashSet<ProgrammeIndicator>();
        }

        public Programme(string name)
            : base()
        {
            Set(name);
        }

        [Required]
        public string Name { get; private set; }

        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<ProgrammeIndicator> Indicators { get; set; }
        public virtual ICollection<UserProgramme> Users { get; set; }

        public void Set(string name)
        {
            Name = name;
        }
    }
}
