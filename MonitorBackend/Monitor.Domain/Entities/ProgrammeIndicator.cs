using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class ProgrammeIndicator : BaseEntity
    {
        public ProgrammeIndicator(int programmeId)
        {
            ProgrammeId = programmeId;
            Values = new HashSet<ProgrammeIndicatorValue>();
        }

        [Required]
        public string Name { get; private set; }
        public string Unit { get; private set; }
        public string Description { get; private set; }

        public int Target { get; private set; }

        public bool IsCumulative { get; private set; }

        public bool IsEnabled { get; private set; }

        public int ProgrammeId { get; private set; }
        public virtual Programme Programme { get; private set; }

        public virtual ICollection<ProgrammeIndicatorValue> Values { get; private set; }

        public void Set(string name, string unit, string description, int target, bool isCumulative)
        {
            Name = name;
            Unit = unit;
            Description = description;
            Target = target;
            IsCumulative = isCumulative;
        }

        public void ToggleIsEnabled()
        {
            IsEnabled = !IsEnabled;
        }
    }
}
