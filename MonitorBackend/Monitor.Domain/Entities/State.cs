using System.Collections.Generic;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class State : BaseEntity
    {
        public State()
        {
            Sites = new HashSet<Site>();
        }

        public State(string name)
            : base()
        {
            Name = name;
        }

        [Required]
        public string Name { get; private set; }
        public MultiPolygon BorderLine { get; private set; }

        public virtual ICollection<Site> Sites { get; set; }

        public void SetBorderLine(MultiPolygon borderLine)
        {
            BorderLine = borderLine;
        }
    }
}
