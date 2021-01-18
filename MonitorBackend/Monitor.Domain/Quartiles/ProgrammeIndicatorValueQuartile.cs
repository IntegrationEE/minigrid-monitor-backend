using Monitor.Domain.Base;
using Monitor.Common.Models;

namespace Monitor.Domain.Quartiles
{
    public class ProgrammeIndicatorValueQuartile : IQuartile
    {
        public double? ValueQ1 { get; set; }
        public double? ValueQ3 { get; set; }
        public Quartile ValueMinMax => new Quartile(ValueQ1, ValueQ3);
    }
}
